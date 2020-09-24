using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenLogger : MonoBehaviour
{
    #region Classes and Enums

    /// <summary>
    /// The log level defines which logs are displayed
    /// </summary>
    public enum LogLevel
    {
        Error, Warn, Info, Debug
    }

    public class ContentRow
    {
        public ContentRow(string text = "", LogLevel loglvl = LogLevel.Info) : this(text, loglvl, System.DateTime.Now) { }
        public ContentRow(string text, LogLevel loglvl, System.DateTime date)
        {
            Text = text;
            LogLvl = loglvl;
            Date = date;
        }

        public System.DateTime Date;
        public LogLevel LogLvl;
        public string Text;

        public override string ToString()
        {
            return "[" + Date.ToString("HH:mm:ss.fff") + "][" + LogLvl.ToString() + "]: " + Text;
        }
    }

    #endregion Classes and Enums

    #region Properties

    public UnityEngine.UI.Text textField;
    public KeyCode debugKey = KeyCode.F;
    public float timeToLive;
    public LogLevel logLvl = LogLevel.Info;

    #endregion Properties

    #region Fields

    private List<ContentRow> _contentList = new List<ContentRow>();
    private const float defaultTimeToLive = 4000.0f;

    #endregion Fields

    #region Events

    void Start()
    {
        timeToLive = defaultTimeToLive;
        Log("ScreenLogger by Daniel Flockert", LogLevel.Info);
    }


    void Update()
    {
        if (Input.GetKeyDown(debugKey))
        {
            Log("Debug Key down.");
        }
    }

    #endregion Events

    #region Public Methods

    public void Debug(string text, float ttl = defaultTimeToLive)
    {
        Log(text, LogLevel.Debug, ttl);
    }


    public void Info(string text, float ttl = defaultTimeToLive)
    {
        Log(text, LogLevel.Info, ttl);
    }


    public void Warn(string text, float ttl = defaultTimeToLive)
    {
        Log(text, LogLevel.Warn, ttl);
    }


    public void Error(string text, float ttl = defaultTimeToLive)
    {
        Log(text, LogLevel.Error, ttl);
    }

    #endregion Public Methods

    #region Private Methods

    private string LogLevelToString(LogLevel loglvl)
    {
        switch (loglvl)
        {
            case LogLevel.Debug:
                return "Debug";
            case (LogLevel.Info):
                return "Info";
            case (LogLevel.Warn):
                return "Warn";
            case (LogLevel.Error):
                return "Error";
            default:
                return "";
        }
    }


    private void Log(string text, LogLevel logLvl = LogLevel.Info, float ttl = 3000.0f)
    {
        StartCoroutine(AddRow(new ContentRow(text), logLvl, timeToLive));
    }


    private IEnumerator AddRow(ContentRow row, LogLevel lvl, float ttl)
    {
        if (lvl >= logLvl)
        {
            _contentList.Add(row);
            updateTextField();
            yield return new WaitForSeconds(ttl / 1000.0f);
            _contentList.Remove(row);
            updateTextField();
        }
        yield return null;
    }


    private void updateTextField()
    {
        string contentString = "";
        for (int i = 0; i < _contentList.Count; i++)
        {
            contentString += _contentList[i].ToString();
            if (i + 1 < _contentList.Count) contentString += "\n";
        }
        textField.text = contentString;
    }

    #endregion Private Methods
}
