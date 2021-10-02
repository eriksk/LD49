using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Rockets;

namespace Assembly_CSharp_Editor.Assets._Project.Scripts.Game.UI
{
    public class RocketUI : MonoBehaviour
    {
        public RocketController Rocket;
        public Text AltitudeValueText;
        public Text VerticalSpeedValueText;

        public Text FuelLeftValueText;
        public Text FuelRightValueText;
        public Text FuelFrontValueText;
        public Text FuelBackValueText;
        public Text FuelMainValueText;

        void Update()
        {
            if (Rocket == null)
            {
                const string na = "N/A";
                AltitudeValueText.text = na;
                VerticalSpeedValueText.text = na;
                FuelLeftValueText.text = na;
                FuelRightValueText.text = na;
                FuelFrontValueText.text = na;
                FuelBackValueText.text = na;
                FuelMainValueText.text = na;
                return;
            }

            var altitude = (int)Rocket.Altitude;
            var verticalSpeed = (int)MpsToKmph(Rocket.VerticalSpeed);

            AltitudeValueText.text = $"{altitude} m";
            VerticalSpeedValueText.text = $"{verticalSpeed} km/h";
            FuelLeftValueText.text = FormatPercent(Rocket.Fuel.Left);
            FuelRightValueText.text = FormatPercent(Rocket.Fuel.Right);
            FuelFrontValueText.text = FormatPercent(Rocket.Fuel.Front);
            FuelBackValueText.text = FormatPercent(Rocket.Fuel.Back);
            FuelMainValueText.text = FormatPercent(Rocket.Fuel.Main);
        }

        private int MpsToKmph(float mps) => (int)Mathf.Abs(mps * 3.6f);
        private string FormatPercent(float value) => $"{(int)(value * 100f)}%";
    }
}