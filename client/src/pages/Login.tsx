import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import type { User } from '../interfaces/user'

export const saveUser = (user: User) => {
    sessionStorage.setItem("user", JSON.stringify(user))
}

export const getUser = (): User | null => {
    const data = sessionStorage.getItem("user")
    return data ? JSON.parse(data) : null
}

export const clearUser = () => {
    sessionStorage.removeItem("user")
}
function Login() {
    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const navigate = useNavigate()

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault()
        setError('')

        try {
            const response = await fetch('https://localhost:7071/api/User/authenticate', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ username, password })
            })

            if (response.ok) {
                const user: User = await response.json();
                saveUser(user)
                window.location.href = '/'
                //navigate('/')
            } else {
                setError('Неверное имя пользователя или пароль.')
            }
        } catch {
            setError('Ошибка подключения к серверу.')
        }
    }

    return (
        <div className="container mt-5" style={{ maxWidth: '400px' }}>
            <h2 className="mb-4">Вход</h2>

            {error && <div className="alert alert-danger">{error}</div>}

            <form onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label htmlFor="username" className="form-label">Имя пользователя</label>
                    <input
                        type="text"
                        className="form-control"
                        id="username"
                        value={username}
                        onChange={e => setUsername(e.target.value)}
                        required
                    />
                </div>

                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Пароль</label>
                    <input
                        type="password"
                        className="form-control"
                        id="password"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        required
                    />
                </div>

                <button type="submit" className="btn btn-primary w-100">Войти</button>
            </form>

            <div className="mt-3 text-center">
                <button className="btn btn-link" onClick={() => navigate('/register')}>
                    Нет аккаунта? Зарегистрироваться
                </button>
            </div>
        </div>
    )
}

export default Login
