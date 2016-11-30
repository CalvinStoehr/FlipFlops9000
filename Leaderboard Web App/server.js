// set up ========================
var express  = require('express');
var app      = express();                               // create our app w/ express
var mysql = require('mysql');                   // mysql for database
var morgan = require('morgan');             // log requests to the console (express4)
var bodyParser = require('body-parser');    // pull information from HTML POST (express4)
var methodOverride = require('method-override'); // simulate DELETE and PUT (express4)


// mysql connection ========================
var connection = mysql.createConnection({
  host: "localhost",
  user: "root",
  password: "pass"
  
});

connection.connect(function(err){
  if(err){
    console.log('Error connecting to Db');
    return;
  }
  console.log('Connection established');
});
connection.query('USE FlipFlops', function(err, rows, fields) {
   if (!err)
     console.log('Using FlipFlops');
   else
     console.log('Error while performing Query.', err);
 });

//app configuration ========================

app.use(express.static(__dirname + '/public'));                 // set the static files location /public/img will be /img for users
app.use(morgan('dev'));                                         // log every request to the console
app.use(bodyParser.urlencoded({'extended':'true'}));            // parse application/x-www-form-urlencoded
app.use(bodyParser.json());                                     // parse application/json
app.use(bodyParser.json({ type: 'application/vnd.api+json' })); // parse application/vnd.api+json as json
app.use(methodOverride());


// routes ======================================================================

    // api ---------------------------------------------------------------------
    // get all

 
app.get('/api/scores',function(req,res){
    connection.query('SELECT * FROM UserStats', function(err, rows){
        if (err)
                res.send(err)
        res.json(rows);
        //res.render('users', {users : rows});
    });
});


// application -------------------------------------------------------------

    app.get('*', function(req, res) {
        res.sendfile('./public/index.html'); // load the single view file (angular will handle the page changes on the front-end)
    });



// listen (start app with node server.js) ======================================
app.listen(3000,function(){
    console.log('Node server running @ http://localhost:3000')
});
