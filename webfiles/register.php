<?php

session_start();

$server_name =  "localhost";
$server_username = "root";
$server_password = "";
$db_name = "flipflops";

$username = $_POST['username'];
$password = $_POST['password'];

// check if username fits requirements
if (strlen($username) > 12) {
	die("Username cannot be more than 12 characters long.");
}
if (strlen($username) < 1) {
	die("Username must be at least 1 character long.");
}

// check if password fits requirements
if (strlen($password) < 8) {
	die("Password must be at least 8 characters long.");
}

// create connection
$conn = new mysqli($server_name, $server_username, $server_password, $db_name);
if (!$conn) {
	die("Connection failed.");
}

// escape unsecure input
$escaped_username =  mysqli_real_escape_string($conn, $_POST['username']);
$hashed_password = password_hash($_POST['password'], PASSWORD_BCRYPT);

// check if username is already registered
$sql = "SELECT * FROM userdata WHERE username='".$escaped_username."'";
$result = mysqli_query($conn, $sql);
if (mysqli_num_rows($result) >= 1) {
	die("Username already registered.");
}

// register user
$sql = "INSERT INTO userdata (username, password_hash)
	VALUES ('".$escaped_username."','".$hashed_password."')";
$result = mysqli_query($conn, $sql);
if (!$result) {
	die("Registration faidled.");
}

$_SESSION["userid"] = mysqli_insert_id($conn);

include("create_auth_token.php");

echo "Registration successful.";

?>