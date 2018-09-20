﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace DAR
{
    public class CharacterProfile
    {
        public int id;
        public string characterName;
        public string logFile;
        public string profileName;
        public bool monitor;
        public string textFontColor;
        public string timerFontColor;
        public string timerBarColor;
        public int volumeValue;
        public int speechRate;
        public string voice;
        private SpeechSynthesizer synth;
        //Constructor
        public CharacterProfile()
        {
            id = 0;
            characterName = "Beastmaster";
            monitor = true;
            textFontColor = "Black";
            timerFontColor = "Blue";
            timerBarColor = "Lime";
            voice = "Microsoft David Desktop";
            volumeValue = 90;
            speechRate = 0;
            synth = new SpeechSynthesizer();
            synth.Rate = speechRate;
            synth.Volume = volumeValue;
            synth.SelectVoice(voice);
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return characterName; }
            set { characterName = value; }
        }
        public string LogFile
        {
            get { return logFile; }
            set { logFile = value; }
        }
        public string ProfileName
        {
            get { return profileName; }
            set { profileName = value; }
        }
        public bool Monitor
        {
            get { return monitor; }
            set { monitor = value; }
        }
        public int VolumeValue
        {
            get { return volumeValue; }
            set { volumeValue = value; }
        }
        public int SpeechRate
        {
            get { return speechRate; }
            set { speechRate = value; }
        }
        public string Voice
        {
            get { return voice; }
            set { voice = value; }
        }
        public string TextFontColor
        {
            get { return textFontColor; }
            set { textFontColor = value; }
        }
        public string TimerFontColor
        {
            get { return timerFontColor; }
            set { timerFontColor = value; }
        }
        public string TimerBarColor
        {
            get { return timerBarColor; }
            set { timerBarColor = value; }
        }
        public void Speak(string output)
        {
            synth.Speak(output);
        }

    }


}
