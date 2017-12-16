# InAsync.Diagnostics.ErrorEventLogTraceListener

`ErrorEventLogTraceListener` はトレース情報のうち、エラーイベントのみをイベントログへ転送する .NET トレースリスナーです。

## Description

.NET Framework にはトレース情報をイベントログへ記録するトレースリスナーとして System.Diagnostics.EventLogTraceListener が用意されていますが、
このリスナーに対して Trace.TraceError メソッドを使用すると、イベント ID が常に 0 となってしまいます。

`ErrorEventLogTraceListener` はエラーイベントをハンドリングし、任意のイベント ID を使用してイベントログに書き込みます。

※ レガシーな Trace クラスの為のトレースリスナーです。[TraceSource](https://docs.microsoft.com/ja-jp/dotnet/api/system.diagnostics.tracesource?view=netframework-4.7.1) を使用できる場合はそちらが推奨されます。

## Usage

ソリューションをビルドして出来た `InAsync.Diagnostics.ErrorEventLogTraceListener.dll` を、トレースが実装されている実行ファイルと同じフォルダに配置します。

config ファイルに下記のような記載を行います。

e.g.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  ...
  <system.diagnostics>
    <trace>
      <listeners>
        <add name="ErrorEventLog"
             type="InAsync.Diagnostics.ErrorEventLogTraceListener,InAsync.Diagnostics.ErrorEventLogTraceListener"
             initializeData=".NET Runtime"
             eventId="1026"
        />
      </listeners>
    </trace>
  </system.diagnostics>
  ...
</configuration>
```

対象の実行ファイルでエラータイプのトレース情報を出力すれば、指定されたイベント ID でイベントログに書き込まれます。

```cs
Trace.TraceError(errorMessage);
```

## Licence

[MIT](https://github.com/in-async/InAsync.Diagnostics.ErrorEventLogTraceListener/blob/master/LICENSE)
