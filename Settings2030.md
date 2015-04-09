# Settings #

This is a brief explanation of the Settings Dialog of version 2.0.3.0.
For older versions, see [bottom](#Older_versions.md) of this page.

![http://redmine-desktop-client.googlecode.com/svn/images/screenshots/redmineclient.settings.2030.png](http://redmine-desktop-client.googlecode.com/svn/images/screenshots/redmineclient.settings.2030.png)
<br><i>The settings dialog</i>
<br>
<table><thead><th> <b>Setting</b> </th><th> <b>Explanation</b> </th></thead><tbody>
<tr><td> <i>Redmine URL</i> </td><td> This is the url to the root page of your redmine server. If you use redmine as a sub-uri, also include this sub-uri. </td></tr>
<tr><td> <i>Requires authentication</i> </td><td> If you need authentication to view and edit the issues and to connect to redmine. </td></tr>
<tr><td> <i>Redmine username</i> </td><td> Your username to authenticate to redmine. </td></tr>
<tr><td> <i>Redmine password</i> </td><td> The password corresponding to your username. </td></tr>
<tr><td> <i>Redmine version</i> </td><td> Select the redmine version, so only available api-calls will be used. </td></tr>
<tr><td> <i>Test Connection</i> </td><td> Test if the entered connection values are correct by setting up a connection.<br>If the connection is successful (and the version is high enough) you can choose the statusses used to close an issue or to set it in progress. </td></tr>
<tr><td> <i>Check for updates on startup</i> </td><td> Check the this project if a newer version is available. </td></tr>
<tr><td> <i>Minimize to Systemtray</i> </td><td> When the application is minimized, hide it from the taskbar. </td></tr>
<tr><td> <i>Minimize on start timer</i> </td><td> When you start the timer to measure your spent time, minimize the window. When <i>minimize to systemtray</i> is enabled, the application will also be hidden from the taskbar. </td></tr>
<tr><td> <i>Pause timer on screen lock and on logoff</i> </td><td> When you lock your screen or logoff, stop and save the timer. When logging in again the timer will continue. </td></tr>
<tr><td> <i>Popup window when minimized</i> </td><td> Popup the application again after N minutes to trigger the user to check he is still working on the same issue or to commit the current time and start a new working period.<br>The popup timer will start when the window is minimized, so the application will popup N minutes after minimization. </td></tr>
<tr><td> <i>Language</i> </td><td> Select the language you want to use the client in. </td></tr>
<tr><td> <i>When closing an issue, set it to</i> </td><td> The status used when you want to close the issue via the 'Close Issue' button in the issue dialog or when you check the 'This also closes the issue' in the submit time dialog </td></tr>
<tr><td> <i>If timer starts and issue has state</i> </td><td> The status used when you want to automatically alter the state of an issue when the timer is started. In the combobox you select the state it must be to be changed automatically. </td></tr>
<tr><td> <i>then set the state to</i> </td><td> The status used when you want to automatically alter the state of an issue when the timer is started. In the combobox you select the state it will be changed to automatically when the issue has the previous selected state. </td></tr>
<tr><td> <i>Edit Enumerations</i> </td><td> Here you can edit the enumerations that are not provided by the Redmine API </td></tr></tbody></table>

<h2>Older versions</h2>
Version 2.0.2.0: <a href='Settings2020.md'>Settings</a><br>
<i>If you use an older version, not all settings are available.</i><br>
If you want to add activities and priorities in an older Redmine Desktop Client, which are not default available in redmine, please refer to <a href='CustomActivitiesAndPriorities.md'>CustomActivitiesAndPriorities</a>.