﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Win32.TaskScheduler
{
	/// <summary>
	/// Collection that contains the actions that are performed by the task.
	/// </summary>
	/// <remarks>A Task Scheduler 1.0 task can only contain a single <see cref="ExecAction"/>.</remarks>
	[XmlRoot("Actions", Namespace = TaskDefinition.tns, IsNullable = false)]
	public sealed class ActionCollection : IEnumerable<Action>, IDisposable, IXmlSerializable
	{
		private V1Interop.ITask v1Task;
		private V2Interop.ITaskDefinition v2Def;
		private V2Interop.IActionCollection v2Coll;

		internal ActionCollection(V1Interop.ITask task)
		{
			v1Task = task;
		}

		internal ActionCollection(V2Interop.ITaskDefinition iTaskDef)
		{
			v2Def = iTaskDef;
			v2Coll = iTaskDef.Actions;
		}

		/// <summary>
		/// Releases all resources used by this class.
		/// </summary>
		public void Dispose()
		{
			v1Task = null;
			v2Def = null;
			v2Coll = null;
		}

		/// <summary>
		/// Adds an action to the task.
		/// </summary>
		/// <param name="action">A derived <see cref="Action"/> class.</param>
		/// <returns>The bound <see cref="Action"/> that was added to the collection.</returns>
		public Action Add(Action action)
		{
			if (v2Def != null)
				action.Bind(v2Def);
			else
				action.Bind(v1Task);
			return action;
		}

		/// <summary>
		/// Adds a new <see cref="Action"/> instance to the task.
		/// </summary>
		/// <param name="actionType">Type of task to be created</param>
		/// <returns>Specialized <see cref="Action"/> instance.</returns>
		public Action AddNew(TaskActionType actionType)
		{
			if (v1Task != null)
				return new ExecAction(v1Task);

			return Action.CreateAction(v2Coll.Create(actionType));
		}

		/// <summary>
		/// Clears all actions from the task.
		/// </summary>
		public void Clear()
		{
			if (v2Coll != null)
				v2Coll.Clear();
			else
				Add(new ExecAction());
		}

		/// <summary>
		/// Determines whether the specified action type is contained in this collection.
		/// </summary>
		/// <param name="actionType">Type of the action.</param>
		/// <returns>
		///   <c>true</c> if the specified action type is contained in this collection; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsType(Type actionType)
		{
			foreach (Action a in this)
				if (a.GetType() == actionType)
					return true;
			return false;
		}

		/// <summary>
		/// Inserts an action at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which action should be inserted.</param>
		/// <param name="action">The action to insert into the list.</param>
		public void Insert(int index, Action action)
		{
			if (v2Coll == null && this.Count > 0)
				throw new NotV1SupportedException("Only a single action is allowed.");

			Action[] pushItems = new Action[this.Count - index];
			for (int i = index; i < this.Count; i++)
				pushItems[i - index] = (Action)this[i].Clone();
			for (int j = this.Count - 1; j >= index; j--)
				RemoveAt(j);
			Add(action);
			for (int k = 0; k < pushItems.Length; k++)
				Add(pushItems[k]);
		}

		/// <summary>
		/// Removes the action at a specified index.
		/// </summary>
		/// <param name="index">Index of action to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">Index out of range.</exception>
		public void RemoveAt(int index)
		{
			if (index >= this.Count)
				throw new ArgumentOutOfRangeException("index", index, "Failed to remove action. Index out of range.");
			if (v2Coll != null)
				v2Coll.Remove(++index);
			else if (index == 0)
				Add(new ExecAction());
			else
				throw new NotV1SupportedException("There can be only a single action and it cannot be removed.");
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the actions in this collection.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the actions in this collection.
		/// </returns>
		public override string ToString()
		{
			if (this.Count == 1)
				return this[0].ToString();
			if (this.Count > 1)
				return Properties.Resources.MultipleActions;
			return string.Empty;
		}

		/// <summary>
		/// Gets or sets a an action at the specified index.
		/// </summary>
		/// <value>The zero-based index of the action to get or set.</value>
		public Action this[int index]
		{
			get
			{
				if (v2Coll != null)
					return Action.CreateAction(v2Coll[++index]);
				if (index == 0)
					return new ExecAction(v1Task.GetApplicationName(), v1Task.GetParameters(), v1Task.GetWorkingDirectory());
				throw new ArgumentOutOfRangeException();
			}
			set
			{
				if (this.Count <= index)
					throw new ArgumentOutOfRangeException("index", index, "Index is not a valid index in the ActionCollection");
				RemoveAt(index);
				Insert(index, value);
			}
		}

		/// <summary>
		/// Gets or sets the identifier of the principal for the task.
		/// </summary>
		/// <exception cref="NotV1SupportedException">Not supported under Task Scheduler 1.0.</exception>
		[System.Xml.Serialization.XmlAttribute(AttributeName = "Context", DataType = "IDREF")]
		public string Context
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.Context;
				return string.Empty;
			}
			set
			{
				if (v2Coll != null)
					v2Coll.Context = value;
				else
					throw new NotV1SupportedException();
			}
		}

		/// <summary>
		/// Gets the number of actions in the collection.
		/// </summary>
		public int Count
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.Count;
				return ((string)v1Task.GetApplicationName()).Length == 0 ? 0 : 1;
			}
		}

		/// <summary>
		/// Gets or sets an XML-formatted version of the collection.
		/// </summary>
		public string XmlText
		{
			get
			{
				if (v2Coll != null)
					return v2Coll.XmlText;
				return XmlSerializationHelper.WriteObjectToXmlText(this);
			}
			set
			{
				if (v2Coll != null)
					v2Coll.XmlText = value;
				else
					XmlSerializationHelper.ReadObjectFromXmlText(value, this);
			}
		}

		/// <summary>
		/// Retrieves an enumeration of each of the actions.
		/// </summary>
		/// <returns>Returns an object that implements the <see cref="IEnumerator"/> interface and that can iterate through the <see cref="Action"/> objects within the <see cref="ActionCollection"/>.</returns>
		public IEnumerator<Action> GetEnumerator()
		{
			if (v2Coll != null)
				return new Enumerator(this);
			return new Enumerator(this.v1Task);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		internal class Enumerator : IEnumerator<Action>
		{
			private V1Interop.ITask v1Task;
			private int v1Pos = -1;
			private IEnumerator v2Enum;
			private ActionCollection parent;

			internal Enumerator(V1Interop.ITask task)
			{
				v1Task = task;
			}

			internal Enumerator(ActionCollection iColl)
			{
				parent = iColl;
				if (iColl.v2Coll != null)
					v2Enum = iColl.v2Coll.GetEnumerator();
			}

			public Action Current
			{
				get
				{
					if (v2Enum != null)
					{
						V2Interop.IAction iAction = v2Enum.Current as V2Interop.IAction;
						if (iAction != null)
							return Action.CreateAction(iAction);
					}
					if (v1Pos == 0)
						return new ExecAction(v1Task.GetApplicationName(), v1Task.GetParameters(), v1Task.GetWorkingDirectory());
					throw new InvalidOperationException();
				}
			}

			/// <summary>
			/// Releases all resources used by this class.
			/// </summary>
			public void Dispose()
			{
				v1Task = null;
				v2Enum = null;
			}

			object System.Collections.IEnumerator.Current
			{
				get { return this.Current; }
			}

			public bool MoveNext()
			{
				if (v2Enum != null)
					return v2Enum.MoveNext();
				return ++v1Pos == 0;
			}

			public void Reset()
			{
				if (v2Enum != null)
					v2Enum.Reset();
				v1Pos = -1;
			}
		}

		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			reader.ReadStartElement("Actions", TaskDefinition.tns);
			while (reader.MoveToContent() == System.Xml.XmlNodeType.Element)
			{
				Action newAction = null;
				switch (reader.LocalName)
				{
					case "Exec":
						newAction = this.AddNew(TaskActionType.Execute);
						break;
					default:
						reader.Skip();
						break;
				}
				if (newAction != null)
					XmlSerializationHelper.ReadObject(reader, newAction);
			}
			reader.ReadEndElement();
		}

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			if (this.Count > 0)
			{
				XmlSerializationHelper.WriteObject(writer, this[0] as ExecAction);
			}
		}
	}
}
