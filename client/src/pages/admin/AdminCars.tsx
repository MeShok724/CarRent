import { useEffect, useState } from 'react'
import type { Car } from '../../interfaces/car';
function AdminCars() {
    const [cars, setCars] = useState<Car[]>([])
    const [editingCar, setEditingCar] = useState<Car | null>(null)
    const [form, setForm] = useState<Partial<Car>>({})
    const [showForm, setShowForm] = useState(false)

    const loadCars = async () => {
        const res = await fetch('https://localhost:7071/api/Cars')
        const data = await res.json()
        setCars(data)
        setShowForm(false)
    }
    useEffect(() => {
        loadCars()
    }, [])
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target
        setForm({ ...form, [name]: value })
    }
    const handleDelete = async (id: number) => {
        if (window.confirm('Удалить машину?')) {
            await fetch(`https://localhost:7071/api/Cars/${id}`, { method: 'DELETE' })
            loadCars()
            setShowForm(false)
        }
    }
    const handleEdit = (car: Car) => {
        setEditingCar(car)
        setForm(car)
        setShowForm(true)
    }
    const handleAddNew = () => {
        setEditingCar(null)
        setForm({})
        setShowForm(true)
    }
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        const method = editingCar ? 'PUT' : 'POST'
        const url = editingCar
            ? `https://localhost:7071/api/Cars/${editingCar.id}`
            : 'https://localhost:7071/api/Cars'

        await fetch(url, {
            method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(form)
        })

        setForm({})
        setEditingCar(null)
        loadCars()
        setShowForm(false)
    }

    return (
        <div className="container mt-4">
            <h2>Машины</h2>

            <button className="btn btn-primary mb-3" onClick={handleAddNew}>
                Добавить машину
            </button>

            {/* Форма */}
            {showForm && (
                <form onSubmit={handleSubmit} className="border rounded p-3 mb-4">
                    <div className="row mb-2">
                        <div className="col">
                            <input
                                type="text"
                                name="brand"
                                placeholder="Марка"
                                value={form.brand || ''}
                                onChange={handleChange}
                                className="form-control"
                                required
                            />
                        </div>
                        <div className="col">
                            <input
                                type="text"
                                name="model"
                                placeholder="Модель"
                                value={form.model || ''}
                                onChange={handleChange}
                                className="form-control"
                                required
                            />
                        </div>
                    </div>

                    <div className="row mb-2">
                        <div className="col">
                            <input
                                type="number"
                                name="year"
                                placeholder="Год"
                                value={form.year || ''}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                        <div className="col">
                            <input
                                type="text"
                                name="vin"
                                placeholder="VIN"
                                value={form.vin || ''}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                    </div>

                    <div className="row mb-2">
                        <div className="col">
                            <input
                                type="text"
                                name="licensePlate"
                                placeholder="Номер"
                                value={form.licensePlate || ''}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                        <div className="col">
                            <input
                                type="text"
                                name="color"
                                placeholder="Цвет"
                                value={form.color || ''}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                    </div>

                    <div className="row mb-3">
                        <div className="col">
                            <input
                                type="number"
                                name="rentalPricePerDay"
                                placeholder="Цена в день"
                                value={form.rentalPricePerDay || ''}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                        <div className="col">
                            <input
                                type="number"
                                name="seats"
                                placeholder="Сидений"
                                value={form.seats || ''}
                                onChange={handleChange}
                                className="form-control"
                            />
                        </div>
                    </div>

                    <button type="submit" className="btn btn-success">
                        {editingCar ? 'Сохранить' : 'Добавить'}
                    </button>
                </form>
            )}

            {/* Таблица */}
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Марка</th>
                        <th>Модель</th>
                        <th>Цвет</th>
                        <th>Цена</th>
                        <th>Действия</th>
                    </tr>
                </thead>
                <tbody>
                    {cars.map((car) => (
                        <tr key={car.id}>
                            <td>{car.id}</td>
                            <td>{car.brand}</td>
                            <td>{car.model}</td>
                            <td>{car.color}</td>
                            <td>{car.rentalPricePerDay} BYN</td>
                            <td>
                                <button className="btn btn-warning btn-sm me-2" onClick={() => handleEdit(car)}>
                                    Изменить
                                </button>
                                <button className="btn btn-danger btn-sm" onClick={() => handleDelete(car.id)}>
                                    Удалить
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default AdminCars