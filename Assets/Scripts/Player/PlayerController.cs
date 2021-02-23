using System;
using UnityEngine;
using Random = UnityEngine.Random;

using C = Const.Constants;
using F = Utils.FictracController;

// This class is the primary player script, it allows the participant to move around.
/// <summary>
/// This class controls the player movement, logs it and also takes care of the player's interaction with the environment, e.g. entering a reward zone 
/// </summary>
namespace wallSystem
{
    public class PlayerController : MonoBehaviour
    {
        public Camera Cam;

        // The stream writer that writes data out to an output file.
        private readonly string _outDir;

        // This is the character controller system used for collision
        private CharacterController _controller;

        // The initial move direction is static zero.
        private Vector3 _moveDirection = Vector3.zero;

        private float _currDelay;

        private float _iniRotation;

        private float _waitTime;

        private bool _playingSound;

        private bool _isStarted = false;

        private bool _reset;
        private int localQuota;

        //string data = "";

        public const float TRANSLATION_GAIN = 1f;
        public const float ROTATION_GAIN = 50f;

        private void Start()
        {
            //transform.position = 
            Random.InitState(DateTime.Now.Millisecond);

            // Choose a random starting angle if the value is not set in trackfile
            _iniRotation = Random.Range(0, 360);
            /* TODO: Add code to check track file for initial rotation of character */

            transform.Rotate(0, _iniRotation, 0);

            try {
                _controller = GetComponent<CharacterController>();
                Cam.transform.Rotate(0, 0, 0);
            }
            catch (NullReferenceException) {
                Debug.LogWarning("Can't set controller object: running an instructional trial");
            }
            _reset = false;

            _isStarted = true;

            Data.LogHeaders();

			F.FictracClient();
		}

        //// Start the character. If init from enclosure, this allows "s" to determine the start position
        //public void ExternalStart(float pickX, float pickY, bool useEnclosure = false)
        //{
        //    while (!_isStarted)
        //    {
        //        Thread.Sleep(20);
        //    }

        //    // No start pos specified so make it random.
        //    if (E.Get().CurrTrial.trialData.StartPosition.Count == 0)
        //    {
        //        // Try to randomly place the character, checking for proximity
        //        // to the pickup location
        //        var i = 0;
        //        while (i++ < 100)
        //        {
        //            var CurrentTrialRadius = DS.GetData().Enclosures[E.Get().CurrTrial.TrialProgress.CurrentEnclosureIndex].Radius;
        //            var v = Random.insideUnitCircle * CurrentTrialRadius * 0.9f;
        //            var mag = Vector3.Distance(v, new Vector2(pickX, pickY));
        //            if (mag > DS.GetData().CharacterData.DistancePickup)
        //            {
        //                transform.position = new Vector3(v.x, 0.5f, v.y);
        //                var camPos = Cam.transform.position;
        //                camPos.y = DS.GetData().CharacterData.Height;
        //                Cam.transform.position = camPos;
        //                return;
        //            }
        //        }
        //        Debug.LogError("Could not randomly place player. Probably due to" +
        //                       " a pick up location setting");
        //    }
        //    else
        //    {
        //        var p = E.Get().CurrTrial.trialData.StartPosition;

        //        if (useEnclosure)
        //        {
        //            p = new List<float>() { pickX, pickY };
        //        }

        //        transform.position = new Vector3(p[0], 0.5f, p[1]);
        //        var camPos = Cam.transform.position;
        //        camPos.y = DS.GetData().CharacterData.Height;
        //        Cam.transform.position = camPos;
        //    }
        //}

        // This is the collision system. TODO: Modify this to suit lab VR
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (!other.gameObject.CompareTag("Pickup")) return;

        //    GetComponent<AudioSource>().PlayOneShot(other.gameObject.GetComponent<PickupSound>().Sound, 10);
        //    Destroy(other.gameObject);

        //    // Tally the number collected per current block
        //    int BlockID = TrialProgress.GetCurrTrial().BlockID;
        //    TrialProgress.GetCurrTrial().TrialProgress.NumCollectedPerBlock[BlockID]++;

        //    TrialProgress.GetCurrTrial().NumCollected++;
        //    E.LogData(
        //        TrialProgress.GetCurrTrial().TrialProgress,
        //        TrialProgress.GetCurrTrial().TrialStartTime,
        //        transform,
        //        1
        //    );

        //    if (--localQuota > 0) return;

        //    E.Get().CurrTrial.Notify();

        //    _playingSound = true;
        //}

        // For Keyboard (arrow keys)
        /*
        private void ComputeMovement() {
			/// Fictrac code snippet begins
			byte[] messageReceived = new byte[1024];
			int byteReceived = sender.Receive(messageReceived);
			string newData = Encoding.ASCII.GetString(messageReceived, 0, byteReceived);

			data += newData;

			int endline = data.IndexOf('\n');
			string line = data.Substring(0, endline);
			data = data.Substring(endline + 1);

			string[] delim = { ", " };
			string[] tokens = line.Split(delim, StringSplitOptions.RemoveEmptyEntries);

			float frameNum = float.Parse(tokens[1]);
			float step_dir = float.Parse(tokens[18]);
			float heading = float.Parse(tokens[17]);
			float step_speed = float.Parse(tokens[19]);
			float forward = float.Parse(tokens[20]);
			float side = float.Parse(tokens[21]);
            float rotation_y = float.Parse(tokens[7]);

			float h = side * TRACK_BALL_RADIUS_M;
			float v = forward * TRACK_BALL_RADIUS_M;

			
			_moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            _moveDirection = transform.TransformDirection(_moveDirection);
			var rotation = Input.GetAxis("Horizontal") * 50f * Time.deltaTime;
            // Keyboard motion control ends

			_controller.Move(_moveDirection * Time.deltaTime);

			transform.Rotate(0, rotation, 0);
		}
        */

        /// <summary>
		/// This function compute the movement of the avatar in the virtual world by compositing the input from 2 streams,
		/// Fictrac and the keyboard
		/// </summary>
        private void ComputeMovement() {
            /// Fictrac code snippet begins
            //string newData = F.ReceiveData();

            //data += newData;

            //int endline = data.IndexOf('\n');
            //string line = data.Substring(0, endline);
            //data = data.Substring(endline + 1);

            //string[] delim = { ", " };
            //string[] tokens = line.Split(delim, StringSplitOptions.RemoveEmptyEntries);

            //float frameNum = float.Parse(tokens[1]);
            //float step_dir = float.Parse(tokens[18]);
            //float heading = float.Parse(tokens[17]);
            //float step_speed = float.Parse(tokens[19]);
            //float forward = float.Parse(tokens[20]);
            //float side = float.Parse(tokens[21]);
            //float rotation_y = float.Parse(tokens[7]);

            F.ReceiveData();

            float h = F.deltaSide * C.TRACK_BALL_RADIUS_M * 0f;
            float v = F.deltaForward * C.TRACK_BALL_RADIUS_M * 0f;

            print("Frame #" + F.frameNumber + "\th: " + h + "\tv: " + v);
            /// Fictrac code snippet ends

            // This calculates the current amount of rotation frame rate independent
            //var rotation = Input.GetAxis("Horizontal") * DS.GetData().CharacterData.RotationSpeed * Time.deltaTime;
            //var rotation = h * DS.GetData().CharacterData.RotationSpeed * Time.deltaTime;
            //var rotation = h * step_speed * Time.deltaTime;
            //var rotation = (heading + step_dir) * Time.deltaTime * ROTATION_GAIN;
            Vector3 trackballTranslation = new Vector3(h, 0, v) * F.toggle;
            Vector3 keyboardTranslation = new Vector3(0, 0, Input.GetAxis("Vertical"));

            float trackballRotation = F.deltaRotationY * F.toggle;
            float keyboardRotation = Input.GetAxis("Horizontal");

            var rotation = (trackballRotation + (keyboardRotation * ROTATION_GAIN)) * Time.deltaTime;     // Compare this with line commented above it. rotation_y is a delta theta whereas
                                                                                                                    // heading and step_dir are absolute values, which caused drifting
                                                                                                                    // Additionally, this also now accepts keyboard input to create a composite rotational movement

			// This calculates the forward speed frame rate independent
			_moveDirection = (trackballTranslation + keyboardTranslation) * TRANSLATION_GAIN; 
            _moveDirection = transform.TransformDirection(_moveDirection);
            //_moveDirection *= DS.GetData().CharacterData.MovementSpeed;
            //_moveDirection *= (step_speed * DS.GetData().CharacterData.MovementSpeed);

            // Here is the movement system
            //const double tolerance = 0.0001;

            // We move iff rotation is 0
            //if (Math.Abs(Mathf.Abs(rotation)) < tolerance)
            _controller.Move(_moveDirection * Time.deltaTime);

            transform.Rotate(0, rotation, 0);
        }

        private void doInitialRotation(){
            var multiplier = 1.0f;
            
            // Smooth out the rotation as we approach the values
            
            var threshold1 = Math.Abs(_currDelay/_waitTime - 0.25f);
            var threshold2 = Math.Abs(_currDelay/_waitTime - 0.75f);

            if (threshold1 < 0.03 || threshold2 < 0.03){
                return;
            }

            if (_currDelay/_waitTime > 0.25 && _currDelay/_waitTime < 0.75){
                multiplier *= -1;
            }

            var anglePerSecond = 240/_waitTime;
            var angle = Time.deltaTime*anglePerSecond;

            transform.Rotate(new Vector3(0, multiplier * angle, 0));
        }

        private void Update() {
            // This first block is for the initial rotation of the character
            if (_currDelay < _waitTime) {
                //doInitialRotation();
            } else {
				// Move the character.
				try {
                    ComputeMovement();
                }
                catch (MissingComponentException) {
                    Debug.LogWarning("Skipping movement calc: instructional trial");
                }
            }

            Data.LogData(transform);
            _currDelay += Time.deltaTime;
        }
    }
}
