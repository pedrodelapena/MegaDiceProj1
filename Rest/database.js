const mysql = require('mysql')

const connection = mysql.createConnection({
	host: 'localhost',
	user: 'root',
	password: '', //input password here
	database: 'players'
});

module.exports = connection;