﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LiftDomain {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Appt_template {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Appt_template() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LiftDomain.Appt.template", typeof(Appt_template).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///	delete from appts where user_id=${user_id}
        ///  .
        /// </summary>
        internal static string delete_appt {
            get {
                return ResourceManager.GetString("delete_appt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to delete from appts where wall_id=${wall_id}.
        /// </summary>
        internal static string delete_from_wall {
            get {
                return ResourceManager.GetString("delete_from_wall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///	select * from (
        ///	select &apos;before&apos; [rel], * from appts a
        ///	where datepart( weekday, dateadd( hour, ${tzoffset}, a.tod_utc)) = ${prev_dow}
        ///	and datepart( hour, dateadd( hour, ${tzoffset}, a.tod_utc)) = ${prev_tod}
        ///	and a.wall_id=${wall_id}
        ///	union
        ///	select &apos;after&apos; [rel], * from appts a
        ///	where datepart( weekday, dateadd( hour, ${tzoffset}, a.tod_utc)) = ${next_dow}
        ///	and datepart( hour, dateadd( hour, ${tzoffset}, a.tod_utc)) = ${next_tod}
        ///	and wall_id=${wall_id}
        ///	) n
        ///	order by 3 asc
        ///  .
        /// </summary>
        internal static string get_adjacent {
            get {
                return ResourceManager.GetString("get_adjacent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///	select id [wall_id] from walls w 
        ///	where
        ///	w.organization_id=${organization_id}
        ///	and w.id not in (
        ///	select wall_id from appts a, walls w2
        ///	where datepart( weekday, dateadd( hour, ${tzoffset}, a.tod_utc)) = ${dow}
        ///	and datepart( hour, dateadd( hour, ${tzoffset}, a.tod_utc)) = ${tod}
        ///	and a.wall_id=w.id
        ///	and w.organization_id=${organization_id}
        ///	)
        ///	.
        /// </summary>
        internal static string get_available_walls {
            get {
                return ResourceManager.GetString("get_available_walls", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select w.title [wall_title], a.tod_utc from appts a, walls w where a.wall_id=w.id and a.user_id=${user_id}.
        /// </summary>
        internal static string get_for_user {
            get {
                return ResourceManager.GetString("get_for_user", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        /// select datepart( weekday, dateadd( hour, ${tzoffset}, a.tod_utc)) [my_dow], datepart( hour, dateadd( hour, ${tzoffset}, a.tod_utc)) [my_tod]  from appts a where user_id=${user_id}
        /// union 
        /// select distinct -1 [my_dow], -1 [my_tod] from appts a where ${user_id} not in (select user_id from appts)
        /// .
        /// </summary>
        internal static string get_my_time_internal {
            get {
                return ResourceManager.GetString("get_my_time_internal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///	select my.my_dow, my.my_tod, thewall.* from
        ///(select wc.total, cal.tod, cal.title,
        ///(wc.total - cal.sunday) [sunday],
        ///(wc.total - cal.monday) [monday],
        ///(wc.total - cal.tuesday) [tuesday],
        ///(wc.total - cal.wednesday) [wednesday],
        ///(wc.total - cal.thursday) [thursday],
        ///(wc.total - cal.friday) [friday],
        ///(wc.total - cal.saturday) [saturday]
        /// from
        ///(
        ///select  T.tod, T.title,
        ///sum(isnull( sun.appt, 0)) [sunday],
        ///sum(isnull( mon.appt, 0)) [monday],
        ///sum(isnull( tue.appt, 0)) [tuesday],
        ///sum(isnull( wed.ap [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string get_stats_internal {
            get {
                return ResourceManager.GetString("get_stats_internal", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to select	u.id [user_id], 
        ///		u.first_name, 
        ///		u.last_name, 
        ///		u.email, 
        ///		isnull(a.id, 0)		[appt_id],
        ///		isnull(a.title, &apos;&apos;) [wall_title],
        ///		isnull(a.dow, 0)	[dow],
        ///		isnull(a.tod, 0)	[tod]
        ///from users u
        ///left outer join 
        ///		(
        ///			select	a2.id [id],
        ///					a2.user_id [user_id],
        ///					datepart( weekday, dateadd( hour, ${tzoffset} /*-6*/, a2.tod_utc)) [dow],
        ///					datepart( hour, dateadd( hour, ${tzoffset} /*-6*/, a2.tod_utc)) [tod],
        ///					w.title [title]
        ///			from
        ///					appts a2,
        ///					walls w
        ///			where
        ///		 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string get_users_like {
            get {
                return ResourceManager.GetString("get_users_like", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///  select T.wall_id, T.tod, T.title,
        ///  isnull( sun.user_id, 0) [sunday_user_id],
        ///isnull( mon.user_id, 0) [monday_user_id],
        ///isnull( tue.user_id, 0) [tuesday_user_id],
        ///isnull( wed.user_id, 0) [wednesday_user_id],
        ///isnull( thu.user_id, 0) [thursday_user_id],
        ///isnull( fri.user_id, 0) [friday_user_id],
        ///isnull( sat.user_id, 0) [saturday_user_id],
        ///isnull( sun.appt, &apos;&apos;) [sunday],
        ///isnull( mon.appt, &apos;&apos;) [monday],
        ///isnull( tue.appt, &apos;&apos;) [tuesday],
        ///isnull( wed.appt, &apos;&apos;) [wednesday],
        ///isnull( thu.appt, &apos;&apos;) [thu [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string get_wall_subscribers {
            get {
                return ResourceManager.GetString("get_wall_subscribers", resourceCulture);
            }
        }
    }
}
