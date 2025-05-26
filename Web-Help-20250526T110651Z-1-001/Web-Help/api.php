<?php
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: GET, POST, PUT, DELETE');
header('Access-Control-Allow-Headers: Content-Type');

$mysqli = new mysqli("localhost", "root", "", "traffic_analisis");

if ($mysqli->connect_error) {
    die(json_encode(['error' => 'Connection failed: ' . $mysqli->connect_error]));
}

$method = $_SERVER['REQUEST_METHOD'];

$inputRaw = file_get_contents("php://input");
$input = json_decode($inputRaw, true) ?? [];

if ($input === null) {
    http_response_code(400);
    echo json_encode(['error' => 'Invalid JSON']);
    exit;
}

switch ($method) {
    case 'GET':
        if (isset($_GET['location_id'])) {
            $location_id = intval($_GET['location_id']);
            $result = $mysqli->query("SELECT * FROM locations WHERE location_id = $location_id");
            $data = $result->fetch_assoc();
            if ($data) {
                http_response_code(200);
                echo json_encode($data);
            } else {
                http_response_code(404);
                echo json_encode(['error' => 'Baleset nem található']);
            }
        } else {
            $result = $mysqli->query("SELECT * FROM locations");
            http_response_code(200);
            echo json_encode($result->fetch_all(MYSQLI_ASSOC));
        }
        break;

    case 'POST':
        if (!isset($input['city'], $input['street'], $input['latitude'], $input['longitude'])) {
            http_response_code(400);
            echo json_encode(['error' => 'Hiányzó kötelező mezők']);
            exit;
        }

        $city = $mysqli->real_escape_string($input['city']);
        $street = $mysqli->real_escape_string($input['street']);
        $latitude = floatval($input['latitude']);
        $longitude = floatval($input['longitude']);

        $stmt = $mysqli->prepare("INSERT INTO locations (city, street, latitude, longitude) VALUES (?, ?, ?, ?)");
        $stmt->bind_param("ssdd", $city, $street, $latitude, $longitude);

        if ($stmt->execute()) {
            http_response_code(201);
            echo json_encode(['message' => "Sikeresen hozzáadva"]);
        } else {
            http_response_code(500);
            echo json_encode(['error' => 'Beszúrás sikertelen: ' . $mysqli->error]);
        }
        $stmt->close();
        break;

    case 'PUT':
        if (!isset($_GET['location_id'])) {
            http_response_code(400);
            echo json_encode(['error' => 'ID megadása szükséges']);
            exit;
        }
        $location_id = intval($_GET['location_id']);

        if (!isset($input['city'], $input['street'], $input['latitude'], $input['longitude'])) {
            http_response_code(400);
            echo json_encode(['error' => 'Hiányzó kötelező mezők']);
            exit;
        }

        $city = $mysqli->real_escape_string($input['city']);
        $street = $mysqli->real_escape_string($input['street']);
        $latitude = floatval($input['latitude']);
        $longitude = floatval($input['longitude']);

        $stmt = $mysqli->prepare("UPDATE locations SET city = ?, street = ?, latitude = ?, longitude = ? WHERE location_id = ?");
        $stmt->bind_param("ssddi", $city, $street, $latitude, $longitude, $location_id);

        if ($stmt->execute()) {
            if ($stmt->affected_rows > 0) {
                http_response_code(200);
                echo json_encode(['message' => "Baleset sikeresen frissítve"]);
            } else {
                http_response_code(404);
                echo json_encode(['error' => 'Baleset nem található vagy nincs változás']);
            }
        } else {
            http_response_code(500);
            echo json_encode(['error' => 'Frissítés sikertelen: ' . $mysqli->error]);
        }
        $stmt->close();
        break;


    case 'DELETE':
        if (!isset($_GET['location_id'])) {
            http_response_code(400);
            echo json_encode(['error' => 'ID megadása szükséges']);
            exit;
        }
        $location_id = intval($_GET['location_id']);

        // Törlés végrehajtása
        if ($mysqli->query("DELETE FROM locations WHERE location_id = $location_id")) {
            if ($mysqli->affected_rows > 0) {
                http_response_code(200);
                echo json_encode(['message' => "Baleset sikeresen törölve"]);
            } else {
                http_response_code(404);
                echo json_encode(['error' => 'Baleset nem található']);
            }
        } else {
            http_response_code(500);
            echo json_encode(['error' => 'Törlés sikertelen']);
        }
        break;

    default:
        http_response_code(405);
        echo json_encode(['error' => 'Hiba! Nem megfelelő metódus']);
        break;
}

$mysqli->close();
