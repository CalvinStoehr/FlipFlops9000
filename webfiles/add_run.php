<?php

session_start();

if (!isset($_SESSION["userid"]))
	die("User not logged in.");

$server_name =  "localhost";
$server_username = "root";
$server_password = "";
$db_name = "flipflops";

// create connection
$conn = new mysqli($server_name, $server_username, $server_password, $db_name);
if (!$conn) {
	die("Connection failed.");
}

// escape unsecure input
$escaped_score = mysqli_real_escape_string($conn, $_POST["score"]);

// check if username/password combo is valid
$sql = "INSERT INTO runs (userid, score) VALUES(".$_SESSION["userid"].", ".$escaped_score.")";
$result = mysqli_query($conn, $sql);
if (!$result) {
	die("Run stats failed to send.");
}

echo "Send successful.";

?>