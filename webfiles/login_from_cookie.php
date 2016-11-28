<?php

session_start();

$server_name =  "localhost";
$server_username = "root";
$server_password = "";
$db_name = "flipflops";

$token = $_POST['token'];

// create connection
$conn = new mysqli($server_name, $server_username, $server_password, $db_name);
if (!$conn) {
	die("Connection failed.");
}

// escape unsecure input
$escaped_token =  mysqli_real_escape_string($conn, $token);

// check if username/password combo is valid
$sql = "SELECT userid FROM auth_tokens WHERE token='".$escaped_token."'";
$result = mysqli_query($conn, $sql);
if (mysqli_num_rows($result) == 0) {
	die($token);
}

$_SESSION["userid"] = mysqli_fetch_field($result);

echo "Login successful.";

?>