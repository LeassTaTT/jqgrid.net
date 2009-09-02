using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess
{
	public DataAccess()
	{
		
	}
    /// <summary>
    /// This method is used by the object data source to get a generic list
    /// of objects to bind to.
    /// </summary>
    /// <param name="p_sortExpression"></param>
    /// <param name="p_sortDirection"></param>
    /// <returns></returns>
    public static List<People> GetData(string p_sortExpression, string p_sortDirection)
    {
        List<People> peoples = new List<People>();
        //Normally here you would access your database to populate this object.
        //Since I want this example not to have any need to connect to a data base
        //I am just creating the data.
        //Note when caching the objectdatasource a chance in parameters being
        //passed in will case the select method to be called again.
        #region Creating data
        peoples.Add(new People(1, "Bart", "Long", "Mower", 10.00M, 18));
        peoples.Add(new People(2, "Al", "Short", "Garbage Man", 18.00M, 23));
        peoples.Add(new People(3, "Phil", "Whinner", "Construction", 23.50M, 22));
        peoples.Add(new People(4, "Bill", "Moan", "Painter", 15.25M, 28));
        peoples.Add(new People(5, "William", "Best", "Consultant", 50.00M, 30));
        peoples.Add(new People(6, "Harry", "Whiler", "Student", 0M, 19));
        peoples.Add(new People(7, "Grace", "Finny", "Store Clerk", 11.50M, 24));
        peoples.Add(new People(8, "Mary", "Willams", "Cell Phone Sales", 19.50M, 32));
        peoples.Add(new People(9, "Jim", "Green", "Teacher", 13.75M, 27));
        peoples.Add(new People(10, "Bob", "RedMan", "Construction", 25.50M, 35));
        peoples.Add(new People(11, "Larry", "Quinn", "Actor", 16.00M, 22));
        peoples.Add(new People(12, "Alex", "Zare", "Construction", 21.25M, 21));
        peoples.Add(new People(13, "Sue", "Andersen", "Waitress", 11.75M, 26));
        peoples.Add(new People(14, "Rita", "Don Diego", "Home maker", 0M, 36));
        peoples.Add(new People(15, "Barry", "Cline", "Repair Man", 19.50M, 40));
        peoples.Add(new People(16, "Tyler", "Huge", "Teacher", 15.50M, 31));
        peoples.Add(new People(17, "Martha", "Smith", "Pre-School Teacher", 12.25M, 38));
        peoples.Add(new People(18, "Mark", "Dalton", "Cook", 14.50M, 35));
        peoples.Add(new People(19, "George", "Larson", "Banker", 30.00M, 45));
        peoples.Add(new People(20, "Tara", "Thomas", "Nurse", 22.00M, 24));
        peoples.Add(new People(21, "Gary", "Black", "Doctor", 46.50M, 29)); 
        #endregion

        //We sort the generic list if requested too
        if (p_sortExpression != null && p_sortExpression != string.Empty)
        {
            peoples.Sort(new PeopleComparer(p_sortExpression));
        }

        //Now that we have sorted check to see if the sort direction is desc
        if (p_sortDirection != null && p_sortDirection == "Desc")
        {
            peoples.Reverse();
        }


        return peoples;
    }
}
