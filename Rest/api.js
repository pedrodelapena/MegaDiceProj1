const express = require('express')
const router = express.Router()
const database = require('./database')

router.get( //ler dados do sql
	'/users', //get all users
	async(req, res, next) => {
		try {
			var command = 'SELECT * FROM users;'
			database.query(command,(err, result)=> {
				if(err){
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)


router.post( //inserir dados no sql
	'/newuser/:name/:password/:nick/:joindate/:lastlogin/:active/:ban/:money', //add a new user
	async(req, res, next) => {
		try {
			var command = "INSERT INTO users VALUES ('" + req.params.name + "','" + req.params.password + "','" + req.params.nick + "','" + req.params.joindate + "','" + req.params.lastlogin + "'," + req.params.active + "," + req.params.ban + "," + req.params.money + ");" 
			database.query(command,(err, result)=> {
				if(err){
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)

router.get(
	'/user/:username', //Get user info by username
	async(req, res, next) => {
		
		try {
			var command = 'SELECT * FROM users WHERE users.login = "' + req.params.username + '";'
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send("Error in SQL section")
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.get(
	'/user/:username/characters', //Get characters from username
	async(req, res, next) => {
		
		try {
			var command = 'SELECT * FROM characters WHERE characters.login = "' + req.params.username + '";'
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send("Error in SQL section")
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}

)

router.post( //inserir dados no sql
	'/newcharacter/:ownername/:str/:dex/:vig/:int/:wis/:cha', //add a new character with setted values
	async(req, res, next) => {
		try {
			var command = "INSERT INTO characters VALUES (0,'" + req.params.ownername + "'," + req.params.str + "," + req.params.dex + "," + req.params.vig + "," + req.params.int + "," + req.params.wis + "," + req.params.cha + ",null,null);" 
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)

router.post( //inserir dados no sql
	'/newnullcharacter/:ownername', //add a new character with null values
	async(req, res, next) => {
		try {
			var command = "INSERT INTO characters VALUES (0,'" + req.params.ownername + "',0,0,0,0,0,0,null,null);" 
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)

router.put( //alterar um objeto inteiro
	'/putcharacter/:id/:strenght/:dexterity/:vigor/:inteligence/:wisdom/:charisma', //change a character atributes
	async(req, res, next) => {
		try {
			var command = "UPDATE characters SET strenght = " + req.params.strenght + " , dexterity = " + req.params.dexterity + " , vigor = " + req.params.vigor + " , inteligence = " + req.params.inteligence + " , wisdom = " + req.params.wisdom + " , charisma = " + req.params.charisma + " WHERE characters.character_id = " + req.params.id + ";"
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.post( //alterar uma tributo de um objeto (coluna) //(Virou Put pq unity)
	'/userlogin/:username/:date', //called to update last login date
	async(req, res, next) => {
		try {
			var command = "UPDATE users SET Last_login = '" + req.params.date + "' WHERE users.login = '" + req.params.username + "';"
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.send({result : "ok"})
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.send("BUGOU") //test to see command
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.patch( //alterar uma tributo de um objeto (coluna)
	'/equipweapon/:charid/:itemid', //called to update the character weapon
	async(req, res, next) => {
		try {
			var command = "UPDATE characters SET weapon = " + req.params.itemid + " WHERE characters.character_id = " + req.params.charid + ";"
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.patch( //alterar uma tributo de um objeto (coluna)
	'/equiparmor/:charid/:itemid', //called to update the character armor
	async(req, res, next) => {
		try {
			var command = "UPDATE characters SET armor = " + req.params.itemid + " WHERE characters.character_id = " + req.params.charid + ";"
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)




router.delete( //quase nunca usar, auto explicativo
	'/deletecharacter/:id', //deletes a character
	async(req, res, next) => {
		try {
			var command = "DELETE FROM characters WHERE characters.character_id = " + req.params.id + ";"
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.post( //inserir dados no sql
	'/newitem/:template/:login/:date', //add a new item to user
	async(req, res, next) => {
		try {
			var command = "INSERT INTO items VALUES (0," + req.params.template + ", '" + req.params.login + "',true,'" + req.params.date + "');" 
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)

router.delete( //deletar dados
	'/deleteitem/:itemid', //delete an item 
	async(req, res, next) => {
		try {
			var command = "DELETE FROM items WHERE items.item_id = " + req.params.itemid + ";" 
			database.query(command,(err, result)=> {
				if(err){
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)

router.get(
	'/item/:username', //Get items from username
	async(req, res, next) => {
		try {
			var command = 'SELECT * FROM items WHERE items.login = "' + req.params.username + '";'
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send("Error in SQL section")
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)
router.get(
	'/item/:username/weapons', //Get items from username that are of type weapons
	async(req, res, next) => {
		try {
			var command = 'Select * from items INNER JOIN item_templates ON items.template_id = item_templates.template_id Where item_templates.item_type = "weapon" AND items.login = "' + req.params.username + '";'
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send("Error in SQL section")
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)
router.get(
	'/item/:username/armor', //Get items from username that are of type armor
	async(req, res, next) => {
		try {
			var command = 'Select * from items INNER JOIN item_templates ON items.template_id = item_templates.template_id Where item_templates.item_type = "armor" AND items.login = "' + req.params.username + '";'
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send("Error in SQL section")
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.get(
	'/templates', //Get all templates
	async(req, res, next) => {
		try {
			var command = 'SELECT * FROM item_templates;'
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send("Error in SQL section")
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.get(
	'/template/:id', //Get template from id
	async(req, res, next) => {
		try {
			var command = 'SELECT * FROM item_templates WHERE item_templates.template_id = ' + req.params.id + ';'
			//res.send(command) //test to see command
			database.query(command,(err, result)=> {
				if(err){
					res.send("Error in SQL section")
					throw err
				} else {
					res.status(200).json({result})
				}
			})
			
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.post( //inserir dados no sql
	'/trade/start', //start transaction
	async(req, res, next) => {
		try {
			var command = "START TRANSACTION;" 
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)
router.post( //inserir dados no sql
	'/trade/rollback', //Rollback
	async(req, res, next) => {
		try {
			var command = "ROLLBACK;" 
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)
router.post( //inserir dados no sql
	'/trade/commit', //Commit
	async(req, res, next) => {
		try {
			var command = "COMMIT;" 
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)

router.post( //inserir dados no sql
	'/trade/:id/:login', //change item user
	async(req, res, next) => {
		try {
			var command = "UPDATE items SET login = '" + req.params.login +"' WHERE items.item_id = " + req.params.id + ";"
			database.query(command,(err, result)=> {
				if(err){
					res.send(command + " is wrong") //test to see command
					throw err
				} else {
					res.status(200).json({result})
				}
			})
		} catch (error) {
			res.status(400).json({error})
		}
	}
)

/*
router.put( //alterar um objeto inteiro
	'/user',
	async(req, res, next) => {
		try {
			res.status(200).json({result: 'ok'})
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)
router.patch( //alterar uma tributo de um objeto (coluna)
	'/user',
	async(req, res, next) => {
		try {
			res.status(200).json({result: 'ok'})
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)
*/

module.exports = router