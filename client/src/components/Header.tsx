import { Link } from 'react-router-dom'
import { useState } from "react"
import type { User } from '../interfaces/user';
import 'bootstrap/dist/css/bootstrap.min.css'

function Header() {
    const handleLogin = () => {
        // Временная заглушка для входа

    }

    const handleLogout = () => {
        setUser(null)
    }

    const [user, setUser] = useState<User | null>(null)

    return (
        <header className="text-bg-dark py-4 d-flex flex-row align-items-center justify-content-between">
            <Link to="/" className="link-light ms-5"><h2>Car Rent</h2></Link>
            <nav className="mx-4">
                <Link to="/" className="link-light">Главная</Link>
                <Link to="/about" className="link-light px-4">О нас</Link>
                    <UserLogic/>
                </nav>
        </header>
    )

    

    function UserLogic() {
        if (!user) return(
            <button className="btn btn-outline-primary" onClick={handleLogin}>
                Войти
            </button>)
        else return (
            <div className="dropdown">
                <button
                    className="btn btn-outline-secondary dropdown-toggle d-flex align-items-center"
                    type="button"
                    id="userDropdown"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                >
                    <i className="bi bi-person-circle me-2"></i>
                    {user?.username}
                </button>
                <ul className="dropdown-menu dropdown-menu-end" aria-labelledby="userDropdown">
                    <li><Link className="dropdown-item" to="/profile">Профиль</Link></li>
                    <li><hr className="dropdown-divider" /></li>
                    <li><button className="dropdown-item" onClick={handleLogout}>Выйти</button></li>
                </ul>
            </div>)
    }
}

export default Header;