using UnityEngine;
using System.Collections;

public class EventName  {

	public string name { get; set;}
	public int id {get; set;}

	public EventName(string name, int id=0){
		this.name = name;
		this.id = id;
	}

	public override int GetHashCode() {
		return (name+id).GetHashCode ();
	}

	public override bool Equals(object obj) {
		EventName other = obj as EventName;
		return name.Equals (other.name) && id == other.id;
	}
}
