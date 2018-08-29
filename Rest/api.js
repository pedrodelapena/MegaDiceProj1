const express = require('express')
const router = express.Router()
const database = require('./database')

router.post( //inserir dados no sql
	'/user',
	async(req, res, next) => {
		try {
			//res.status(200).json({result: 'ok'})
		} catch (error) {
			console.log("DEU RUIM")
			res.status(400).json({error})
		}
	}
)

router.get( //ler dados do sql
	'/user',
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
router.delete( //quase nunca usar, auto explicativo
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

module.exports = router