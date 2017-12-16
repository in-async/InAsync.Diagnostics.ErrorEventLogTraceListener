using System.Diagnostics;

namespace InAsync.Diagnostics {

    /// <summary>
    /// エラーイベントのみをイベントログに転送するトレースリスナー。
    /// </summary>
    public class ErrorEventLogTraceListener : TraceListener {
        private const string EVENT_ID = "eventId";
        private readonly EventLogTraceListener _listener;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:スコープを失う前にオブジェクトを破棄")]
        public ErrorEventLogTraceListener(string source) {
            _listener = new EventLogTraceListener(source) {
                Filter = new EventTypeFilter(SourceLevels.Error),
            };
        }

        /// <summary>
        /// 書き込み先となるイベントログ。
        /// </summary>
        public EventLog EventLog => _listener.EventLog;

        /// <summary>
        /// トレースリスナーの名前。
        /// </summary>
        public override string Name {
            get { return _listener.Name; }
            set { _listener.Name = value; }
        }

        /// <summary>
        /// イベントログに書き込む際のイベント ID。
        /// </summary>
        public int EventId {
            get {
                int eventId;
                return int.TryParse(Attributes[EVENT_ID], out eventId) ? eventId : 0;
            }
        }

        /// <summary>
        /// サポートされるカスタム属性。
        /// </summary>
        /// <returns></returns>
        protected override string[] GetSupportedAttributes() => new[] { EVENT_ID };

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            try {
                _listener.Dispose();
            }
            finally {
                base.Dispose(disposing);
            }
        }

        /// <summary>
        /// イベントログを終了します。
        /// </summary>
        public override void Close() => _listener.Close();

        /// <summary>
        /// 何もしません。
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string message) { }

        /// <summary>
        /// 何もしません。
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string message) { }

        /// <summary>
        /// イベント情報をイベントログに書き込みます。
        /// </summary>
        /// <param name="eventCache"></param>
        /// <param name="source"></param>
        /// <param name="eventType"></param>
        /// <param name="id"></param>
        /// <param name="message"></param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message) {
            _listener.TraceEvent(eventCache, source, eventType, EventId, message);
        }

        /// <summary>
        /// イベント情報をイベントログに書き込みます。
        /// </summary>
        /// <param name="eventCache"></param>
        /// <param name="source"></param>
        /// <param name="eventType"></param>
        /// <param name="id"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args) {
            _listener.TraceEvent(eventCache, source, eventType, EventId, format, args);
        }

        /// <summary>
        /// イベント情報をイベントログに書き込みます。
        /// </summary>
        /// <param name="eventCache"></param>
        /// <param name="source"></param>
        /// <param name="eventType"></param>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data) {
            _listener.TraceData(eventCache, source, eventType, EventId, data);
        }

        /// <summary>
        /// イベント情報をイベントログに書き込みます。
        /// </summary>
        /// <param name="eventCache"></param>
        /// <param name="source"></param>
        /// <param name="eventType"></param>
        /// <param name="id"></param>
        /// <param name="data"></param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data) {
            _listener.TraceData(eventCache, source, eventType, EventId, data);
        }
    }
}