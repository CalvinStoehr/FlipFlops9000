<?php

// make sure necessary variables are set
if (!isset($conn) || !isset($_SESSION["userid"]))
	die("Token creation has failed.");

// generate token
$token = bin2hex(random_bytes(20));


// add token to database
$sql = "INSERT INTO auth_tokens(token, userid) VALUES('".$token."', ".$_SESSION["userid"].")";
$result = mysqli_query($conn, $sql);
if (!$result)
	die($token);

// store cookie for two weeks
setcookie("auth_cookie", $token, time()+60*60*24*7*2);

?>