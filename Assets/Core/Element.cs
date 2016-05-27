using UnityEngine;
using System.Collections;
using System.Collections.Generic;




/// <summary>
/// Extension of the element class to handle different BaseApplication types.
/// </summary>
/// <typeparam name="T"></typeparam>
public class Element<T> : Element where T : BaseApplication
{
	/// <summary>
	/// Returns app as a custom 'T' type.
	/// </summary>
	new public T app { get { return (T)base.app; } }
}


/// <summary>
/// Base class for all MVC related classes.
/// </summary>
/// 
public class Element : MonoBehaviour {
		
		/// <summary>
		/// Reference to the root application of the scene.
		/// </summary>
		public BaseApplication app { get { return m_app = Assert<BaseApplication>(m_app, true); } }
		private BaseApplication m_app;
		
		public T Assert<T>(T p_var) where T : Object 
		{	
		  	return Assert (p_var, false);
		}
		
		/// <summary>
		/// Finds a instance of 'T' if 'var' is null. Returns 'var' otherwise.
		/// If 'global' is 'true' searches in all scope, otherwise, searches in childrens.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="p_var"></param>
		/// <param name="p_global"></param>
		/// <returns></returns>
		public T Assert<T>(T p_var, bool p_global) where T : Object 
		{
			return p_var == null ? (p_global ? GameObject.FindObjectOfType<T>() : transform.GetComponentInChildren<T>()) : p_var;            
		}
		
		/// <summary>
		/// Sends a notification to all controllers passing this instance as 'target' and some 'data'.
		/// </summary>
		/// <param name="p_event"></param>
		/// <param name="p_data"></param>
		public void Notify(string p_event,params object[] p_data) { app.Notify(p_event, this, p_data); }
		
		/// <summary>
		/// Sends a notification to all controllers, after 'delay', passing this instance as 'target' and some 'data'.
		/// </summary>
		/// <param name="p_delay"></param>
		/// <param name="p_event"></param>
		/// <param name="p_data"></param>
		public void Notify(float p_delay,string p_event,params object[] p_data) { app.Notify(p_delay,p_event, this, p_data); }
		
		/// <summary>
		/// Logs a message using this element information.
		/// </summary>
		/// <param name="p_msg"></param>
		public void Log(object p_msg, int p_verbose)
		{
			//Only outputs logs equal or bigger than the application 'verbose' level.
			if (p_verbose <= app.verbose) Debug.Log(GetType().Name + "> " + p_msg);
		}
		
}

