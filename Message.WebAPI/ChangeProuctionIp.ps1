# get the directory of this script file
$currentDirectory = [IO.Path]::GetDirectoryName($MyInvocation.MyCommand.Path)
# get the full path and file name of the Web.config file in the same directory as this script
$appConfigFile = [IO.Path]::Combine($currentDirectory, 'Web.config')
# initialize the xml object
$appConfig = New-Object XML
# load the config file as an xml object
$appConfig.Load($appConfigFile)
# iterate over the settings
foreach($connectionString in $appConfig.configuration.connectionStrings.add)
{
    # write the name to the console
    'name: ' + $connectionString.name
    # write the connection string to the console
    'connectionString: ' + $connectionString.connectionString
    # change the connection string
    $connectionString.connectionString = 'metadata=res://*/MsgCenterModel.csdl|res://*/MsgCenterModel.ssdl|res://*/MsgCenterModel.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=10.1.1.5;user id=superadmin;password=admin;persistsecurityinfo=True;Charset=utf8;database=ttt&quot;'
}
foreach($ipaddress in $appConfig.configuration.MyActiveMQ.mqidaddress)
{
    'mqidaddress: ' + $ipaddress
}
$appConfig.configuration.MyActiveMQ.mqidaddress= 'tcp://10.1.1.5:61616/'
# save the updated config file
$appConfig.Save($appConfigFile)