import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import type { Car } from '../interfaces/car'
import CarImageCarousel from '../components/CarImageCarousel'

const CarPage = () => {
    const { id } = useParams<{ id: string }>()
    const [car, setCar] = useState<Car | null>(null)

    useEffect(() => {
        if (!id) return

        fetch(`https://localhost:7071/api/cars/${id}`)
            .then(res => res.json())
            .then(data => setCar(data))
            .catch(err => console.error('Ошибка загрузки:', err))
    }, [id])

    if (!car) return <div className="text-center mt-5">Загрузка...</div>

    return (
        <div className="container mt-5">
            <div className="card mb-3 shadow">
                <div className="card-body container">
                    <h3 className="card-title pt-3 ps-3">
                        {car.brand} {car.model} ({car.year})
                    </h3>
                    <div className="row pt-4 ps-2">
                        <div className="col-md-7">
                            <CarImageCarousel carImages={car.carImages} />
                        </div>
                        <p className="card-text col-md-5 ps-5">
                            <strong>Цвет:</strong> {car.color} <br />
                            <strong>Пробег:</strong> {car.mileage} км <br />
                            <strong>Категория:</strong> {car.category?.name}<br />
                            <strong>Объем двигателя:</strong> {car.engineVolume} л<br />
                            <strong>Кол-во сидений:</strong> {car.seats}<br />
                            <strong>Цена аренды:</strong> {car.rentalPricePerDay} BYN / день<br />

                            <button className="btn btn-primary mt-5 px-4" onClick={handleRentClick}>
                                Арендовать
                            </button>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    )
}
const handleRentClick = () => {
    // Пока пусто: логика аренды будет добавлена позже
    console.log("Арендовать нажато");
};

export default CarPage