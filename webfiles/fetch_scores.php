<?php

$server_name =  "localhost";
$username = "root";
$password = "";
$db_name = "flipflops";

// create connection
$conn = new mysqli($server_name, $username, $password, $db_name);
if (!$conn) {
	die("Connection Failed.");
}

// use connection to fetch results
$sql = "SELECT userdata.username, runs.score FROM runs INNER JOIN userdata ON runs.userid=userdata.userid ORDER BY runs.score DESC LIMIT 10";

$result = mysqli_query($conn, $sql);

if (!$result)
	die("Fetch failed.");

// give results
$names = "";
$scores = "";

while ($row = mysqli_fetch_row($result)) {
	$names .= $row[0].PHP_EOL;
	$scores .= $row[1].PHP_EOL;
}

echo "Success!".PHP_EOL.$names."$$||$$".PHP_EOL.$scores;

?>