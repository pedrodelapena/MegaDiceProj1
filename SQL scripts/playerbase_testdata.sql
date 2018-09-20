USE players;

INSERT INTO users VALUES ('lightclawjl','768899551','lightclaw','2018/08/28',null,true,false,0);
INSERT INTO users VALUES ('pedrodelapena','PauloDeLaPedro','SLEGHART','2018/08/29',null,true,false,800);

select * from users;

INSERT INTO item_templates VALUES (0,'Sword of the meek','weapon',50,20,3);
INSERT INTO item_templates VALUES (0,'Crossbow','weapon',30,30,1);
INSERT INTO item_templates VALUES (0,'Short Sword','weapon',30,20,0);

INSERT INTO item_templates VALUES (0,'Leather Armor','armor',11,0,0);
INSERT INTO item_templates VALUES (0,'Thunder Blessing','armor',20,40,4);

INSERT INTO item_templates VALUES (0,'Lesser Restoration Potion','Consumable',20,1,0);

SELECT * FROM item_templates;

INSERT INTO items VALUES (0,1,'lightclawjl',false,'2018/08/28');
INSERT INTO items VALUES (0,2,'lightclawjl',false,'2018/08/28');
INSERT INTO items VALUES (0,3,'pedrodelapena',false,'2018/08/28');
INSERT INTO items VALUES (0,4,'pedrodelapena',false,'2018/08/28');
INSERT INTO items VALUES (0,5,'lightclawjl',false,'2018/08/28');
INSERT INTO items VALUES (0,6,'pedrodelapena',false,'2018/08/28');

SELECT * FROM items;

INSERT INTO characters VALUES (0,'lightclawjl',-1,5,0,0,3,3,1,5);
INSERT INTO characters VALUES (0,'pedrodelapena',4,1,3,-1,1,2,2,null);

SELECT * FROM characters;
