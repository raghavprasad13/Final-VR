using UnityEngine;

namespace Utils {
	public class NeuralynxController {

		private static SerialController serialController = null;

		private static void Init() {
			serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
		}

		public static void StartNeuralynxArduino() {
			Init();
			serialController.SendSerialMessage("1");
		}

		public static void StopNeuralynxArduino() {
			if (serialController == null)
				return;

			serialController.SendSerialMessage("0");
			serialController = null;
		}
	}
}