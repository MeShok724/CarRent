---------------------------  1. Получить все машины по категории: +

CREATE OR REPLACE FUNCTION get_cars_by_category(category_id INT)
RETURNS SETOF "Cars" AS $$
BEGIN
  RETURN QUERY
  SELECT *
  FROM "Cars"
  WHERE "CategoryId" = category_id;
END;
$$ LANGUAGE plpgsql;
------------------------------------   2. Добавить пользователя в черный список: +

CREATE OR REPLACE PROCEDURE add_to_blacklist(user_id INT, reason TEXT, days INT)
AS $$
BEGIN
  INSERT INTO "Blacklists"("UserId", "Reason", "BunnedAt", "ExpirationDate")
  VALUES (user_id, reason, now(), now() + (days || ' days')::interval);
END;
$$ LANGUAGE plpgsql;

---------------------------------------  3. Получить сумму всех оплат за заказ: +

CREATE OR REPLACE FUNCTION get_order_total_payments(order_id INT)
RETURNS NUMERIC AS $$
DECLARE
  total NUMERIC;
BEGIN
  SELECT COALESCE(SUM("Amount"), 0) INTO total
  FROM "Payments"
  WHERE "OrderId" = order_id;

  RETURN total;
END;
$$ LANGUAGE plpgsql;

---------------------------------------  4. Получить список свободных машин (со статусом Available) +

CREATE OR REPLACE FUNCTION get_available_cars()
RETURNS SETOF "Cars" AS $$
BEGIN
  RETURN QUERY
  SELECT c.*
  FROM "Cars" c
  JOIN "CarStatuses" s ON c."StatusId" = s."Id"
  WHERE s."Name" = 'Available';
END;
$$ LANGUAGE plpgsql;

----------------------------------------  5. Продление страховки автомобиля на заданное количество месяцев +

CREATE OR REPLACE PROCEDURE extend_car_insurance(car_id INT, months_to_add INT)
AS $$
BEGIN
  UPDATE "Insurance"
  SET "ExpirationDate" = "ExpirationDate" + (months_to_add || ' months')::interval
  WHERE "CarId" = car_id AND "ExpirationDate" > CURRENT_DATE;
END;
$$ LANGUAGE plpgsql;