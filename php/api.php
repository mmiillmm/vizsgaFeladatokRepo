<?php
header("Content-Type: application/json; charset=utf-8");
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS");

require_once "connect.php";

$method = $_SERVER['REQUEST_METHOD'];
$id = isset($_GET['id']) ? (int) $_GET['id'] : null;

function sanitize($conn, $value)
{
    return mysqli_real_escape_string($conn, $value);
}

switch ($method) {
    case 'GET':
        if ($id) {
            $result = $conn->query("SELECT * FROM products WHERE id = $id");
            if ($result && $result->num_rows > 0) {
                echo json_encode([
                    "status" => "success",
                    "data" => $result->fetch_assoc()
                ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
            } else {
                http_response_code(404);
                echo json_encode([
                    "status" => "error",
                    "message" => "A megadott azonosítóval nem található termék: $id"
                ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
            }
        } else {
            $result = $conn->query("SELECT * FROM products");
            $products = [];
            while ($row = $result->fetch_assoc()) {
                $products[] = $row;
            }
            echo json_encode([
                "status" => "success",
                "data" => $products
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
        }
        break;

    case 'POST':
        $data = json_decode(file_get_contents("php://input"), true);
        if (
            !isset($data['name'], $data['description'], $data['price'], $data['image_name'])
        ) {
            http_response_code(400);
            echo json_encode([
                "status" => "error",
                "message" => "Hiányos adatok. Kérjük, töltse ki a név, leírás, ár és kép mezőket."
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
            break;
        }

        $name = sanitize($conn, $data['name']);
        $desc = sanitize($conn, $data['description']);
        $price = (float) $data['price'];
        $img = sanitize($conn, $data['image_name']);

        $conn->query("INSERT INTO products (name, description, price, image_name)
                    VALUES ('$name', '$desc', $price, '$img')");
        $newId = $conn->insert_id;

        http_response_code(201);
        echo json_encode([
            "status" => "success",
            "message" => "Termék sikeresen létrehozva.",
            "id" => $newId
        ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
        break;

    case 'PUT':
        if (!$id) {
            http_response_code(400);
            echo json_encode([
                "status" => "error",
                "message" => "Hiányzik a termék azonosító (id)."
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
            break;
        }

        $data = json_decode(file_get_contents("php://input"), true);
        if (
            !isset($data['name'], $data['description'], $data['price'], $data['image_name'])
        ) {
            http_response_code(400);
            echo json_encode([
                "status" => "error",
                "message" => "Hiányos adatok. Kérjük, töltse ki a név, leírás, ár és kép mezőket."
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
            break;
        }

        $name = sanitize($conn, $data['name']);
        $desc = sanitize($conn, $data['description']);
        $price = (float) $data['price'];
        $img = sanitize($conn, $data['image_name']);

        $result = $conn->query("UPDATE products SET name='$name', description='$desc', price=$price, image_name='$img' WHERE id=$id");

        if ($conn->affected_rows > 0) {
            echo json_encode([
                "status" => "success",
                "message" => "Termék sikeresen frissítve."
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
        } else {
            http_response_code(404);
            echo json_encode([
                "status" => "error",
                "message" => "A megadott azonosítóval nem található termék: $id"
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
        }
        break;

    case 'DELETE':
        if (!$id) {
            http_response_code(400);
            echo json_encode([
                "status" => "error",
                "message" => "Hiányzik a termék azonosító (id)."
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
            break;
        }

        $conn->query("DELETE FROM products WHERE id = $id");

        if ($conn->affected_rows > 0) {
            http_response_code(204);
        } else {
            http_response_code(404);
            echo json_encode([
                "status" => "error",
                "message" => "A megadott azonosítóval nem található termék: $id"
            ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
        }
        break;

    default:
        http_response_code(405);
        echo json_encode([
            "status" => "error",
            "message" => "Nem támogatott HTTP metódus."
        ], JSON_PRETTY_PRINT | JSON_UNESCAPED_UNICODE);
        break;
}
?>