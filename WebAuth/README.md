Base Role-based access control for Asp.net Web App
================

## Features
  - Code generation with T4MVC
  - Logging with Elmah
  - Logging with Common.Logging
  - Security with AntiXSS
  - PushMessage with specific user Id
  - Real time Logging and PushMessage with SignalR

## Dependency
  - ActiveMQ 5.91 or later
  - MySQL 
  - Microsoft.AspNet.SignalR

## Database Logging
Due to NLog database logging base on ADO.NET,  You need to execute following script in Mysql:
-- ----------------------------
-- Table structure for `Logs`
-- ----------------------------
DROP TABLE IF EXISTS `Logs`;
CREATE TABLE `Logs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CreateDate` datetime NOT NULL,
  `LogLevel` varchar(5) NOT NULL,
  `CallSite` varchar(5000) DEFAULT NULL,
  `Massage` longtext,
  `StackTrace` varchar(5000) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

## License
(The MIT License)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 'Software'), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
