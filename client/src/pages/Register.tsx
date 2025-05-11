import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import type { User } from '../interfaces/user'
import { saveUser } from './Login'

function Register() {
    const [username, setUsername] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const navigate = useNavigate()

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault()
        setError('')

        const user: Partial<User> = {
            username,
            email,
            passwordHash: password,
            createTime: new Date(),
            statusId: 1 // Присваиваем, например, статус "Активный"
        }

        try {
            const response = await fetch('https://localhost:7071/api/User', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(user)
            })

            if (response.ok) {
                const user: User = await response.json();
                saveUser(user)
                window.location.href = '/'
                //navigate('/')
            } else {
                setError('Ошибка регистрации. Возможно, пользователь уже существует.')
            }
        } catch {
            setError('Ошибка подключения к серверу.')
        }
    }

    return (
        <div className="container mt-5" style={{ maxWidth: '400px' }}>
            <h2 className="mb-4">Регистрация</h2>

            {error && <div className="alert alert-danger">{error}</div>}

            <form onSubmit={handleRegister}>
                <div className="mb-3">
                    <label className="form-label">Имя пользователя</label>
                    <input
                        type="text"
                        className="form-control"
                        value={username}
                        onChange={e => setUsername(e.target.value)}
                        required
                    />
                </div>

                <div className="mb-3">
                    <label className="form-label">Email</label>
                    <input
                        type="email"
                        className="form-control"
                        value={email}
                        onChange={e => setEmail(e.target.value)}
                        required
                    />
                </div>

                <div className="mb-3">
                    <label className="form-label">Пароль</label>
                    <input
                        type="password"
                        className="form-control"
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        required
                    />
                </div>

                <button type="submit" className="btn btn-success w-100">Зарегистрироваться</button>
            </form>

            <div className="mt-3 text-center">
                <button className="btn btn-link" onClick={() => navigate('/login')}>
                    Уже есть аккаунт? Войти
                </button>
            </div>
        </div>
    )
}

export default Register
