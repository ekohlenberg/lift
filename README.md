# lift
Lift is a tool for facilitating a 24x7 prayer ministry.

# introduction 

Lift is intended for an organization to structure a 24x7 ministry derived from Isaiah 62.  The notion of putting prayer requests online is to enable others to intercede on behalf of those in need of prayer.  There are lots of sites on the web that provide this sort of functionality. The Lift tool is unique in that it encourages accountability among the participants in the ministry through the concept of the prayer wall, in which participants receive a call from the person who has the prior "watch", and then participant calls the person who has the next "watch". 

Some goals of the project are to enable a multi-organization, multi-location system in which an organization anywhere can simply go to the site, sign up, upload a logo, and have their own prayer site.  Also, because organizations and users are located throughout the world, the system will store all times in UTC and display time according to preference.  Furthermore, the system will provide multi-language support.  As we develop the project, we will be creating a Spanish version of all the copy to ensure that we don't miss anything.  Users will be able to see the website copy according to preference.  Preference for time and language will be prioritized by user first, then organization.  In other words, organizations will have a default language and time zone.  So when an anonymous user logs into the site, they will see language and time according to the org's preference.  When the login, they will be able to override the org preference.

# folder structure

The development folder is structured as 
Lift 
   LiftCommon      - The persistence mechanism and a few helper classes 
   LiftDomain      - A library that has a 1:1 correspondence to the database schema.   
   LiftApp         - The application itself. 
   LiftRoot        - A application that implements an HttpModule that redirects based on subdomain 
   db              - Contains the database schema as orginally received from Sugar Creek, plus additional update scripts 


# application globals

As of now, there are 4 "global" instances: Organization, User, Language, and LiftTime.  Each class will implement a static, read-only property called "Current" to return the current object in context.  The current org and user will be stored in the Session by its primary key which is then used as a key to a static Hashtable.  The Language will reference the other two to figure out which is the current language for the session.  The LiftTime class actually has a property call CurrentTime. See the Time_Zone_Handling page for more info.

In Request.aspx, you will see "Language.Current.<some property>".  This is what is used instead of hardcoding the copy.  More phrases can be added by inserting into the phrases table and then adding a corresponding property to the Language class.

# persistence layer

The persistence layer is simple but requires some introduction.  The core concept is the Hashtable. In this system, data "tuples" are represented by "soft" objects where the tuple elements (database fields) appear as name/value pairs in the Hashtable.   The ModelObject is the "generic tuple" so to speak.  There are derivations of it in the LiftDomain library, one each corresponding to each table in the schema. 
ModelObject has an data member that implements its persistence mechanism.  LiftCommon implements only the database mechanism.  There is also a web service version that was not brought over. 
The ModelObject system has only to "verbs", or operations or methods:  doCommand, and doQuery.  doCommand implements operations that has side-effects and returns a scalar.  doQuery implements selects that return a result set.  doCommand and doQuery take an action string.  The action string represents a persistence method, ModelObject method, or SQL template.  Which one is determined by the programmer and order of precedence: 
   If there is a public method in the ModelObject-derived class, then it is invoked. 
   Else if there is a method of the same name in the persistence class, it is invoked. 
   Else if there is a sql template of the same name associated with the ModelObject-derived class, it is loaded and invoked. 

The DatabasePersistence class implements "select", "insert", "update", "delete", and "save" (combination of update/insert).  They operate based on the metadata (BaseTable and PrimaryKey) in the ModelObject-derived classes.  In the simplest case, you can define a ModelObject-derived class along with the appropriate metadata, and read and write it from and to the database immediately.  By setting properties on the ModelObject class (assigning name-value pairs), you are "dirtying" the object.  The persistence layer takes the assigned named-value pairs and uses them to build the sql statement to send to the database.  It is possible to mess up and incorrectly spell a field name. Therefore, for Lift, data members that act as database fields have been added.  They don't do much but hopefully they help reduce coding errors.

# time zone handling

From a policy standpoint, all times are stored in the database in UTC.  However, all times are _displayed_ in the application in either the user's time_zone preference or the organization's time_zone preference. 

This is implemented through the use of two classes in the LiftDomain library:  LiftTime and UserTimeProperty.

LiftTime provides the current time in the user's time zone for display purposes.  It has a static property, CurrentTime, that returns the current time adjusted for either the organization or user in context.

UserTimeProperty derives from and replaces the use of DateTimeProperty in the domain library.  Formerly, every field in the database that is a datetime field was represented by the DateTimeProperty data type.  Now, the same fields are represented by the UserTimeProperty type.  The .Value property expects the database to store the datetime in UTC. The "get" therefore adjusts the stored UTC time to the org/user's preferred time.  The "set" expects the incoming value to be in the org/user's preferred time, and adjusts the time to UTC automatically.  Do not set the UserTimeProperty.Value property using a UTC time (unless UTC is the preferred time zone).

Example:

class MyObject : BaseLiftDomain
{
 UserTimeProperty my_time;
 .
 .
 .
}

// to set the property to the current time:
MyObject myObject = new MyObject();
myObject.my_time.Value = LiftTime.CurrentTime;  // current time returns time in preferred time zone


// to assign the date/time to some UI control:
myTextBox.Text = myObject.my_time.Value.ToString(); // automatically adjusts to preferred time zone

// we can localize the datetime format also...
myTextBox.Text = myObject.my_time.Value.ToString( Language.Current.datetimeformat );

# schema change policy

Schema changes will need to be added to an update_nn.sql script in the db folder.  When we go live, we will take the original reference schema then apply the updates in succession. The updates can include structure and data.  If you look at them, you will see some small schema changes I've made plus some changes to the static data, including the language support.  The exception will be the identity column info.  I will create scripts to enable and disable identity properties across the schema.  When we go live, we will have as many as four organizations at once.  Their separate schemas will be merged into a single schema, updates applied, then identities turned on as the last step.  That's the plan, anyway.


