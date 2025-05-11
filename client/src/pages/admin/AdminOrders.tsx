import { useEffect, useState } from 'react'
import type { Order } from '../../interfaces/order'
import type { User } from '../../interfaces/user'
import type { Car } from '../../interfaces/car'
import type { Branch } from '../../interfaces/branch'
import type { Employee } from '../../interfaces/employee'

function AdminOrders() {
    const [orders, setOrders] = useState<Order[]>([])
    const [form, setForm] = useState<Partial<Order>>({})
    const [editingOrder, setEditingOrder] = useState<Order | null>(null)
    const [showForm, setShowForm] = useState(false)

    const [users, setUsers] = useState<User[]>([])
    const [cars, setCars] = useState<Car[]>([])
    const [branches, setBranches] = useState<Branch[]>([])
    const [employees, setEmployees] = useState<Employee[]>([])

    const loadOrders = async () => {
        const res = await fetch('https://localhost:7071/api/Order')
        const data = await res.json()
        setOrders(data)
    }

    const loadOptions = async () => {
        const [u, c, b, e] = await Promise.all([
            fetch('https://localhost:7071/api/User').then(r => r.json()),
            fetch('https://localhost:7071/api/Cars').then(r => r.json()),
            fetch('https://localhost:7071/api/Branches').then(r => r.json()),
            fetch('https://localhost:7071/api/Employee').then(r => r.json())
        ])
        setUsers(u)
        setCars(c)
        setBranches(b)
        setEmployees(e)
    }

    useEffect(() => {
        loadOrders()
        loadOptions()
    }, [])

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target
        setForm({ ...form, [name]: value })
    }

    const handleDelete = async (id: number) => {
        if (window.confirm('Удалить заказ?')) {
            await fetch(`https://localhost:7071/api/Order/${id}`, { method: 'DELETE' })
            loadOrders()
        }
    }

    const handleEdit = (order: Order) => {
        setEditingOrder(order)
        setForm(order)
        setShowForm(true)
    }

    const handleAddNew = () => {
        setEditingOrder(null)
        setForm({})
        setShowForm(true)
    }

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        const method = editingOrder ? 'PUT' : 'POST'
        const url = editingOrder
            ? `https://localhost:7071/api/Order/${editingOrder.id}`
            : 'https://localhost:7071/api/Order'

        await fetch(url, {
            method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(form)
        })

        setForm({})
        setEditingOrder(null)
        setShowForm(false)
        loadOrders()
    }

    return (
        <div className="container mt-4">
            <h2>Заказы</h2>

            <button className="btn btn-primary mb-3" onClick={handleAddNew}>
                Добавить заказ
            </button>

            {showForm && (
                <form onSubmit={handleSubmit} className="border rounded p-3 mb-4">
                    <div className="row mb-2">
                        <div className="col">
                            <label>Клиент</label>
                            <select name="customerId" className="form-select" value={form.customerId || ''} onChange={handleChange}>
                                <option value="">Выберите клиента</option>
                                {users.map(u => (
                                    <option key={u.id} value={u.id}>{u.username}</option>
                                ))}
                            </select>
                        </div>
                        <div className="col">
                            <label>Автомобиль</label>
                            <select name="carId" className="form-select" value={form.carId || ''} onChange={handleChange}>
                                <option value="">Выберите авто</option>
                                {cars.map(c => (
                                    <option key={c.id} value={c.id}>{c.brand} {c.model}</option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <div className="row mb-2">
                        <div className="col">
                            <label>Филиал выдачи</label>
                            <select name="branchFromId" className="form-select" value={form.branchFromId || ''} onChange={handleChange}>
                                <option value="">Выберите</option>
                                {branches.map(b => (
                                    <option key={b.id} value={b.id}>{b.name}</option>
                                ))}
                            </select>
                        </div>
                        <div className="col">
                            <label>Филиал возврата</label>
                            <select name="branchToId" className="form-select" value={form.branchToId || ''} onChange={handleChange}>
                                <option value="">Выберите</option>
                                {branches.map(b => (
                                    <option key={b.id} value={b.id}>{b.name}</option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <div className="row mb-2">
                        <div className="col">
                            <label>Сотрудник</label>
                            <select name="employeeId" className="form-select" value={form.employeeId || ''} onChange={handleChange}>
                                <option value="">Выберите</option>
                                {employees.map(e => (
                                    <option key={e.id} value={e.id}>{e.firstname} {e.lastname}</option>
                                ))}
                            </select>
                        </div>
                        <div className="col">
                            <label>Цена</label>
                            <input
                                type="number"
                                name="priceTotal"
                                className="form-control"
                                value={form.priceTotal || ''}
                                onChange={handleChange}
                            />
                        </div>
                    </div>

                    <div className="row mb-2">
                        <div className="col">
                            <label>Дата начала</label>
                            <input
                                type="date"
                                name="startDate"
                                className="form-control"
                                value={form.startDate?.toString().slice(0, 10) || ''}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="col">
                            <label>Дата возврата</label>
                            <input
                                type="date"
                                name="returnDate"
                                className="form-control"
                                value={form.returnDate?.toString().slice(0, 10) || ''}
                                onChange={handleChange}
                            />
                        </div>
                    </div>

                    <div className="mb-3">
                        <input
                            type="text"
                            name="status"
                            className="form-control"
                            placeholder="Статус"
                            value={form.status || ''}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="mb-3">
                        <textarea
                            name="notes"
                            className="form-control"
                            placeholder="Заметки"
                            value={form.notes || ''}
                            onChange={handleChange}
                        />
                    </div>

                    <button type="submit" className="btn btn-success">
                        {editingOrder ? 'Сохранить' : 'Добавить'}
                    </button>
                </form>
            )}

            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Клиент</th>
                        <th>Машина</th>
                        <th>Филиал</th>
                        <th>Сотрудник</th>
                        <th>Цена</th>
                        <th>Статус</th>
                        <th>Действия</th>
                    </tr>
                </thead>
                <tbody>
                    {orders.map(o => (
                        <tr key={o.id}>
                            <td>{o.id}</td>
                            <td>{o.customer?.username}</td>
                            <td>{o.car?.brand} {o.car?.model}</td>
                            <td>{o.branchFrom?.name}</td>
                            <td>{o.employee?.firstname}</td>
                            <td>{o.priceTotal} BYN</td>
                            <td>{o.status}</td>
                            <td>
                                <button className="btn btn-warning btn-sm me-2" onClick={() => handleEdit(o)}>Изменить</button>
                                <button className="btn btn-danger btn-sm" onClick={() => handleDelete(o.id)}>Удалить</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default AdminOrders
