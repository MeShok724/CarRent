-----------------------------------   1. Блокировка заказов от пользователей из чёрного списка +

CREATE OR REPLACE FUNCTION check_blacklist_before_insert() 
RETURNS TRIGGER AS $$
BEGIN
    -- Проверка наличия user_id в таблице blacklist
    IF EXISTS (
        SELECT 1 FROM "Blacklists" WHERE UserId = NEW."CustomerId"
    ) THEN
        RAISE EXCEPTION 'Пользователь % находится в черном списке и не может оформить заказ.', NEW."CustomerId";
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Шаг 2: Создание триггера
CREATE TRIGGER trg_check_blacklist
BEFORE INSERT ON "Orders"
FOR EACH ROW
EXECUTE FUNCTION check_blacklist_before_insert();


----------------------------------------  2. Обновление времени UpdatedAt в Cars при любом изменении: +

CREATE OR REPLACE FUNCTION update_car_timestamp()
RETURNS TRIGGER AS $$
BEGIN
  NEW."UpdatedAt" := now();
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_car_timestamp
BEFORE UPDATE ON "Cars"
FOR EACH ROW
EXECUTE FUNCTION update_car_timestamp();


-------------------------------  3. Автоматическое создание записи в CarRentHistory после нового заказа: +

CREATE OR REPLACE FUNCTION insert_rent_history()
RETURNS TRIGGER AS $$
BEGIN
  INSERT INTO "CarRentHistory"("CarId", "CustomerId", "OrderId", "StartDate", "EndDate")
  VALUES (NEW."CarId", NEW."CustomerId", NEW."Id", NEW."StartDate", NEW."ReturnDate");
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_add_rent_history
AFTER INSERT ON "Orders"
FOR EACH ROW
EXECUTE FUNCTION insert_rent_history();

------------------------------------- 4.  Автоматическая установка статуса "Busy" у автомобиля при создании заказа +

CREATE OR REPLACE FUNCTION set_car_busy_on_order()
RETURNS TRIGGER AS $$
BEGIN
  UPDATE "Cars"
  SET "StatusId" = (SELECT "Id" FROM "CarStatuses" WHERE "Name" = 'Busy')
  WHERE "Id" = NEW."CarId";
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_set_car_busy
AFTER INSERT ON "Orders"
FOR EACH ROW
EXECUTE FUNCTION set_car_busy_on_order();

-----------------------------------  5. Автоматическая установка текущей даты в ReturnDate, если она не указана при создании заказа +

CREATE OR REPLACE FUNCTION set_default_return_date()
RETURNS TRIGGER AS $$
BEGIN
  IF NEW."ReturnDate" IS NULL THEN
    NEW."ReturnDate" := NEW."StartDate" + INTERVAL '1 day';
  END IF;
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_set_default_return_date
BEFORE INSERT ON "Orders"
FOR EACH ROW
EXECUTE FUNCTION set_default_return_date();


