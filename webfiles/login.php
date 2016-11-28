<?php

session_start();

$server_name =  "localhost";
$server_username = "root";
$server_password = "";
$db_name = "flipflops";

$username = $_POST['username'];
$password = $_POST['password'];

// create connection
$conn = new mysqli($server_name, $server_username, $server_password, $db_name);
if (!$conn) {
	die("Connection failed.");
}

// escape unsecure input
$escaped_username =  mysqli_real_escape_string($conn, $_POST['username']);
$password = $_POST['password'];

// check if username/password combo is valid
$sql = "SELECT password_hash, userid FROM userdata WHERE username='".$escaped_username."'";
$result = mysqli_query($conn, $sql);
if (mysqli_num_rows($result) == 0) {
	die("Username does not exist.");
}

$stored_hash = mysqli_fetch_field($result);
if (!password_verify($password, $stored_hash)) {
	die("Username/password combo does not exist.");
}

$_SESSION["userid"] = mysqli_fetch_field($result);

include("create_auth_token.php");

echo "Login successful.";

?>