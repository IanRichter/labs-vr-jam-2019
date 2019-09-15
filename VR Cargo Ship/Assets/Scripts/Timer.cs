using UnityEngine;

public class Timer {

	private float totalDuration = 0f;
	private float progress = 0f;

	public Timer(float duration) {
		totalDuration = duration;
		Reset();
	}

	public void Reset() {
		progress = totalDuration;
	}

	public void Tick() {
		progress += Time.deltaTime;
	}

	public bool IsDone {
		get {
			return progress >= totalDuration;
		}
	}

}
