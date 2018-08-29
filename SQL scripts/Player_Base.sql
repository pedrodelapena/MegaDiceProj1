DROP DATABASE IF EXISTS players;
CREATE DATABASE players;
USE players;

CREATE TABLE users (
	login VARCHAR(16) NOT NULL,
	user_password VARCHAR(64) NOT NULL,
	nick VARCHAR(16) UNIQUE NOT NULL,
    join_date DATE NOT NULL,
    last_login DATE,
    active BOOL NOT NULL,
    banned BOOL NOT NULL,
    money_spent INT,
    PRIMARY KEY (login)
);

CREATE TABLE item_templates (
	template_id INT NOT NULL AUTO_INCREMENT,
    item_name VARCHAR(32) NOT NULL,
    item_type VARCHAR(16) NOT NULL,
    amount_1 INT,
    amount_2 INT,
    rarity INT NOT NULL,
    PRIMARY KEY (template_id)
);

CREATE TABLE items (
	item_id INT NOT NULL AUTO_INCREMENT,
    template_id INT NOT NULL,
    login VARCHAR(16) NOT NULL,
    equip BOOL,
    create_date DATE,
    PRIMARY KEY (item_id),
	CONSTRAINT item_templates FOREIGN KEY (template_id)
        REFERENCES item_templates (template_id),
	CONSTRAINT item_user FOREIGN KEY (login)
        REFERENCES users (login)
);

CREATE TABLE characters (
	character_id INT NOT NULL AUTO_INCREMENT,
	login VARCHAR(16) NOT NULL,
    strenght INT NOT NULL,
    dexterity INT NOT NULL,
    vigor INT NOT NULL,
    inteligence INT NOT NULL,
    wisdom INT NOT NULL,
    charisma INT NOT NULL,
	weapon INT,
    armor INT,
    PRIMARY KEY (character_id),
	CONSTRAINT character_user FOREIGN KEY (login)
        REFERENCES users (login),
	CONSTRAINT character_weapon FOREIGN KEY (weapon)
        REFERENCES items (item_id),
	CONSTRAINT character_armor FOREIGN KEY (armor)
        REFERENCES items (item_id)
);
