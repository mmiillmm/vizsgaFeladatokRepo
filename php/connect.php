<?php
$servername = "localhost";
$username = "root";
$password = "";
$database = "product_catalog";

$conn = new mysqli($servername, $username, $password, $database);

if ($conn->connect_error) {
    die("Kapcsolódási hiba: " . $conn->connect_error);
} else {
    echo "Sikeres kapcsolat az adatbázissal!";
}
?>
