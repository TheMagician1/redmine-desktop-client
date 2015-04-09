# Custom Activities and Priorities #

_From version 2.0.2.0 and higher you can edit those xml-files via the settings dialog._


Because the redmine Rest API does not provide the enumerations used in issues and time management for activities and priorities, you have to add them yourself.

After you have set the correct settings to connect to redmine, three new files have been created in the same directory as the executable (the start directory).

  * Activities.xml
  * DocumentCategories.xml
  * IssuePriorities.xml

### Activities ###
In the _Activities.xml_ you can find the activities you can use for time recording. The application needs the _Id_ of the activities that are available in redmine. You should ask your redmine administrator for the _Ids_ of the activities. Then you can edit the xml-file to contain the new Id's.<br>
<br>
The default content of the xml file is:<br>
<code>&lt;Activities&gt;&lt;IdentifiableName id="8" name="Design" /&gt;&lt;IdentifiableName id="9" name="Development" /&gt;&lt;/Activities&gt;</code><br>
<br>
To add the new activities, you need to add the following string for each additional activity:<br>
<code>&lt;IdentifiableName id="&lt;ID&gt;" name="&lt;NAME&gt;" /&gt;</code><br>
where <br>
<br>
<ID><br>
<br>
<i>is the Id of the activity and</i>

<NAME>

<i>is the name of the activity as shown in the activity dropdownbox.</i>

So for example, if you want to add the activity testing with id 11 the content of the file would be:<br>
<code>&lt;Activities&gt;&lt;IdentifiableName id="8" name="Design" /&gt;&lt;IdentifiableName id="9" name="Development" /&gt;&lt;IdentifiableName id="11" name="Testing" /&gt;&lt;/Activities&gt;</code><br>


<h3>Document Categories</h3>
The <i>DocumentCategories.xml</i> is not actively used, but still created.<br>
<br>
<h3>Issue Priorities</h3>
In the <i>IssuePriorities.xml</i> you can find the priorities that can be assigned to issues. The application needs the <i>Id</i> of the priorities that are available in redmine. You should ask your redmine administrator for the <i>Ids</i> of the priorities. Then you can edit the xml-file to contain the new Id's.<br>
<br>
For an explanation on how to add the new priorities, please check the Activities.