using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// PeopleComparer is used to sort the generic collection of the People class
/// </summary>
public class PeopleComparer : IComparer<People>
{
    #region Constructor
    public PeopleComparer(string p_propertyName)
    {
        //We must have a property name for this comparer to work
        this.PropertyName = p_propertyName;
    } 
    #endregion

    #region Property
    private string _propertyName;

    public string PropertyName
    {
        get { return _propertyName; }
        set { _propertyName = value; }
    } 
    #endregion


    #region IComparer<People> Members

    /// <summary>
    /// This comparer is used to sort the generic comparer
    /// The constructor sets the PropertyName that is used
    /// by reflection to access that property in the object to 
    /// object compare.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int Compare(People x, People y)
    {
        Type t = x.GetType();
        PropertyInfo val = t.GetProperty(this.PropertyName);
        if (val != null)
        {
            return Comparer.DefaultInvariant.Compare(val.GetValue(x,null), val.GetValue(y,null));
        }
        else
        {
            throw new Exception(this.PropertyName + " is not a valid property to sort on.  It doesn't exist in the Class.");
        }
    }

    #endregion
}
