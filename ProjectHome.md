## Introduction ##

This project will try to port the old RedmineClient to use the REST API of Redmine. It uses the Redmine Net API to implement the communication.

The current supported features:
  * Measuring time
  * Pause Measuring on screen lock / continue on unlock
  * Pause Measuring on logoff / continue on logon
  * View issues by project
  * Create new issues for a project
  * View/Edit Issue details (including Attachments, children, parents and relations)
  * Possible to set issue status to 'In Progress' on start timer
  * Possible to close issue on committing time. Or set issue to another state.
  * Add notes on changing an issue (also when using above two options)
  * View/Edit spent time on an issue
  * Basic view for custom fields of an issue


## Translation ##
All main dialogs use text elements that can be translated.
If you want to help translate, we set up a project at [Transifex](https://www.transifex.com/projects/p/rdc/):

You can view the current languages and the progress of their translation.<br>
If your language is not yet available, please create an account on transifex and add your language to our project.<br>
If your language is available, and you want to alter/add translations you can also create an account and ask to be joined to the language.<br>
<img src='https://www.transifex.com/projects/p/rdc/resource/language/chart/image_png?image.png' /><br>
<a href='https://www.transifex.com/projects/p/rdc/resource/language/'><img src='https://ds0k0en9abmn1.cloudfront.net/static/charts/images/tx-logo-micro.646b0065fce6.png' /></a>

<h2>More information</h2>

More information on Redmine: <a href='http://www.redmine.org'>http://www.redmine.org</a>

More information on the original RedmineClient: <a href='http://redmineclient.sourceforge.net/'>http://redmineclient.sourceforge.net/</a>

More information on the Redmine Net API: <a href='http://code.google.com/p/redmine-net-api/'>http://code.google.com/p/redmine-net-api/</a>