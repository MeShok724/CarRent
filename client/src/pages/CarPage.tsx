import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import type { Car } from '../interfaces/car'
import CarImageCarousel from '../components/CarImageCarousel'
import type { User } from '../interfaces/user'
import { getUser } from './Login'

const CarPage = () => {
    const { id } = useParams<{ id: string }>()
    const [car, setCar] = useState<Car | null>(null)
    const [showModal, setShowModal] = useState(false)
    const [startDate, setStartDate] = useState('')
    const [returnDate, setReturnDate] = useState('')
    const [user] = useState<User | null>(getUser())


    useEffect(() => {
        if (!id) return
        fetch(`https://localhost:7071/api/cars/${id}`)
            .then(res => res.json())
            .then(data => setCar(data))
            .catch(err => console.error('Ошибка загрузки:', err))
    }, [id])

    const handleOrderSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        if (!car || !user) return

        const order = {
            customerId: user.id,
            carId: car.id,
            branchFromId: car.branchId,
            branchToId: car.branchId,
            employeeId: null,
            startDate,
            returnDate,
            priceTotal: car.rentalPricePerDay, // или расчет
            discountId: null,
            status: 'Pending',
            notes: '',
            createdAt: new Date().toISOString()
        }

        const response = await fetch('https://localhost:7071/api/order', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(order)
        })

        if (response.ok) {
            alert('Заказ успешно создан')
            setShowModal(false)
        } else {
            alert('Ошибка при создании заказа')
        }
    }

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
                        <div className="card-text col-md-5 ps-5">
                            <strong>Цвет:</strong> {car.color} <br />
                            <strong>Пробег:</strong> {car.mileage} км <br />
                            <strong>Категория:</strong> {car.category?.name}<br />
                            <strong>Объем двигателя:</strong> {car.engineVolume} л<br />
                            <strong>Кол-во сидений:</strong> {car.seats}<br />
                            <strong>Цена аренды:</strong> {car.rentalPricePerDay} BYN / день<br />

                            <button className="btn btn-primary mt-5 px-4" onClick={() => setShowModal(true)}>
                                Арендовать
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            {/* Модальное окно */}
            {showModal && (
                <div className="modal d-block" tabIndex={-1} style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
                    <div className="modal-dialog">
                        <form onSubmit={handleOrderSubmit}>
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Оформление аренды</h5>
                                    <button type="button" className="btn-close" onClick={() => setShowModal(false)}></button>
                                </div>
                                <div className="modal-body">
                                    <div className="mb-3">
                                        <label>Дата начала аренды</label>
                                        <input
                                            type="date"
                                            className="form-control"
                                            value={startDate}
                                            onChange={(e) => setStartDate(e.target.value)}
                                            required
                                        />
                                    </div>
                                    <div className="mb-3">
                                        <label>Дата окончания аренды</label>
                                        <input
                                            type="date"
                                            className="form-control"
                                            value={returnDate}
                                            onChange={(e) => setReturnDate(e.target.value)}
                                            required
                                        />
                                    </div>
                                </div>
                                <div className="modal-footer">
                                    <button type="submit" className="btn btn-success">Подтвердить</button>
                                    <button type="button" className="btn btn-secondary" onClick={() => setShowModal(false)}>Отмена</button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    )
}

export default CarPage