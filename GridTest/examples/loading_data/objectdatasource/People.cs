using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for People
/// </summary>
public class People : ICloneable 
{
    #region Constructors
    /// <summary>
    /// We have a generic list of this type in our example
    /// </summary>
    public People()
    { }
    public People(int p_key, string p_firstName, string p_lastName, string p_skills,
        decimal p_dollarsPerHour, int p_age)
    {
        this.Key = p_key;
        this.Age = p_age;
        this.DollarsPerHour = p_dollarsPerHour;
        this.FirstName = p_firstName;
        this.LastName = p_lastName;
        this.Skills = p_skills;
    } 
    #endregion

    #region Properties
    private int _key;

    public int Key
    {
        get { return _key; }
        set { _key = value; }
    }
    private string _firstName;

    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    private string _lastName;

    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    private string _skills;

    public string Skills
    {
        get { return _skills; }
        set { _skills = value; }
    }

    private decimal _dollarsPerHour;

    public decimal DollarsPerHour
    {
        get { return _dollarsPerHour; }
        set { _dollarsPerHour = value; }
    }

    private int _age;

    public int Age
    {
        get { return _age; }
        set { _age = value; }
    } 
    #endregion

    #region ICloneable Members

    public Object Clone()
    {
        People people = new People();
        people.Key = this.Key;
        people.Age = this.Age;
        people.DollarsPerHour = this.DollarsPerHour;
        people.FirstName = this.FirstName;
        people.LastName = this.LastName;
        people.Skills = this.Skills;

        return people;
    }

    #endregion
}
