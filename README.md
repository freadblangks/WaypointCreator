WaypointCreator
===============

Uses text output file from wpp to create paths from movement packets.

Maximus Instructions:

grab the Minimap.zip from https://drive.google.com/open?id=0B5LRVWxUgf9EaGxudFhHZ0dhYnc and place into the root directory of waypointcreator repository in order to provide textures for the viewer.

Running the Application:

login into sql database, (recommended as this will provide creature_template data)
![alt text](https://snag.gy/r2RGMp.jpg)

initial screen will load with directx viewer and waypoint manager
![alt text](https://snag.gy/VdSZr7.jpg)

import a sniff:
![alt text](https://snag.gy/FT23va.jpg)

Select map from the viewer by rick clicking on the viewer screen:
![alt text](https://snag.gy/58VI6i.jpg)

viewer functions:

F2 - Zoom Out
F3 - Zoom In
Arrow Up - Spectator will move Up
Arrow Left - Spectator will move Left
Arrow Right - Spectator will move Right
Arrow Down - Spectator will move Down

manager grid functions:

clicking on column headers will sort the rows accordingly.
filter values by name or entry which will auto recognize which is numeric or alpha
specify filter by StartsWith EndsWith or Contains (contains is default)

click check box of selected item to include in viewer
you can include as many as you want.
![alt text](https://snag.gy/dlDUkV.jpg)
![alt text](https://snag.gy/qo2fv6.jpg)

the waypoint values are editable and you can add new waypoints which will reflect in viewer
![alt text](https://snag.gy/OvfHyq.jpg)

sql exports to udb either to file or to clipboard by right clicking on the item
![alt text](https://snag.gy/3twW1J.jpg)

Exported sql Sample:
```MySQL
-- Pathing for Zabra'jin Guard Entry: 18909 'UDB FORMAT'
@GUID := XXXXXX;
UPDATE `creature` SET `position_x`=200.1622,`position_y`=7864.784,`position_z`=43.6102 WHERE `guid`=@GUID;
DELETE FROM `creature_movement` WHERE `id`=@GUID;
INSERT INTO `creature_movement` (`id`,`point`,`position_x`,`position_y`,`position_z`,`waittime`,`script_id`,`textid1`,`textid2`,`textid3`,`textid4`,`textid5`,`emote`,`spell`,`orientation`,`model1`,`model2`) VALUES
(@GUID,1,200.1622,7864.784,43.6102,0,0,0,0,0,0,0,0,0,0,0,0),
(@GUID,2,205.1164,7847.752,35.51953,0,0,0,0,0,0,0,0,0,0,0,0),
(@GUID,3,201.3893,7861.518,41.31487,0,0,0,0,0,0,0,0,0,0,0,0),
(@GUID,4,202.1393,7858.518,39.81487,0,0,0,0,0,0,0,0,0,0,0,0),
(@GUID,5,202.8893,7855.518,38.56487,0,0,0,0,0,0,0,0,0,0,0,0),
(@GUID,6,203.8893,7852.018,37.06487,0,0,0,0,0,0,0,0,0,0,0,0),
(@GUID,7,204.8893,7848.518,36.06487,0,0,0,0,0,0,0,0,0,0,0,0),
(@GUID,8,240,7500,0,0,0,0,0,0,0,0,0,0,0,0,0);
-- .go xyz 200.1622 7864.784 43.6102
```






