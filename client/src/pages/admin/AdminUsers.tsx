import { useEffect, useState } from 'react'
import type { User } from '../../interfaces/user'

function AdminUsers() {
    const [users, setUsers] = useState<User[]>([])
    const [statuses, setStatuses] = useState<{ id: number; name: string }[]>([])
    const [editingUser, setEditingUser] = useState<User | null>(null)
    const [form, setForm] = useState<Partial<User>>({})
    const [showForm, setShowForm] = useState(false)

    const loadUsers = async () => {
        const res = await fetch('https://localhost:7071/api/User')
        const data = await res.json()
        setUsers(data)
    }
    const loadRoles = async () => {
        const res = await fetch('https://localhost:7071/api/UserStatus')
        const data = await res.json()
        setStatuses(data)
    }

    useEffect(() => {
        loadUsers()
        loadRoles()
    }, [])

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target
        setForm({ ...form, [name]: value })
    }

    const handleDelete = async (id: number) => {
        if (window.confirm('Удалить пользователя?')) {
            await fetch(`https://localhost:7071/api/User/${id}`, { method: 'DELETE' })
            loadUsers()
        }
    }

    const handleEdit = (user: User) => {
        setEditingUser(user)
        setForm(user)
        setShowForm(true)
    }

    const handleAddNew = () => {
        setEditingUser(null)
        setForm({})
        setShowForm(true)
    }

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()

        const method = editingUser ? 'PUT' : 'POST'
        const url = editingUser
            ? `https://localhost:7071/api/User/${editingUser.id}`
            : 'https://localhost:7071/api/User'

        await fetch(url, {
            method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(form)
        })

        setForm({})
        setEditingUser(null)
        setShowForm(false)
        loadUsers()
    }

    return (
        <div className="container mt-4">
            <h2>Пользователи</h2>

            {/*<button className="btn btn-primary mb-3" onClick={handleAddNew}>*/}
            {/*    Добавить пользователя*/}
            {/*</button>*/}

            {showForm && (
                <form onSubmit={handleSubmit} className="border rounded p-3 mb-4">
                    <div className="mb-2">
                        <input
                            type="text"
                            name="username"
                            placeholder="Имя пользователя"
                            value={form.username || ''}
                            onChange={handleChange}
                            className="form-control"
                            required
                        />
                    </div>
                    <div className="mb-2">
                        <input
                            type="email"
                            name="email"
                            placeholder="Email"
                            value={form.email || ''}
                            onChange={handleChange}
                            className="form-control"
                            required
                        />
                    </div>
                    <div className="mb-2">
                        <select
                            name="statusId"
                            value={form.statusId || ''}
                            onChange={(e) => setForm({ ...form, statusId: parseInt(e.target.value) })}
                            className="form-select"
                            required
                        >
                            <option value="">Выберите роль</option>
                            {statuses.map(status => (
                                <option key={status.id} value={status.id}>{status.name}</option>
                            ))}
                        </select>
                    </div>
                    <button type="submit" className="btn btn-success">
                        {editingUser ? 'Сохранить' : 'Добавить'}
                    </button>
                </form>
            )}

            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Имя</th>
                        <th>Email</th>
                        <th>Дата</th>
                        <th>Статус</th>
                        <th>Действия</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map((u) => (
                        <tr key={u.id}>
                            <td>{u.id}</td>
                            <td>{u.username}</td>
                            <td>{u.email}</td>
                            <td>{new Date(u.createTime).toLocaleDateString()}</td>
                            <td>{u.status?.name}</td>
                            <td>
                                <button className="btn btn-warning btn-sm me-2" onClick={() => handleEdit(u)}>
                                    Изменить
                                </button>
                                <button className="btn btn-danger btn-sm" onClick={() => handleDelete(u.id)}>
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

export default AdminUsers
