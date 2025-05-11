import { Link, useNavigate } from 'react-router-dom'
import { useState } from "react"
import type { User } from '../interfaces/user';
import 'bootstrap/dist/css/bootstrap.min.css'
import { getUser, clearUser } from '../pages/Login';

function Header() {
    const navigate = useNavigate()
    const handleLogin = () => {
        navigate('/login')
    }

    const handleLogout = () => {
        clearUser()
        window.location.href = '/login'
        //navigate('/login')
    }

    const [user] = useState<User | null>(getUser())

    return (
        <header className="text-bg-dark p-4 d-flex flex-row align-items-center justify-content-between">
            {/*<Link to="/" className="link-light ms-5"><h2>Car Rent</h2></Link>*/}
            <h2>Car Rent</h2>
            <nav className="mx-4">
                <Link to="/" className="link-light">Доступные машины</Link>
                <Link to="/filials" className="link-light px-4">Филиалы</Link>
            </nav>
            <AdminLogic />
            <UserLogic />
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
    function AdminLogic() {
        if (user?.status?.name === 'admin')
            return (<div className="dropdown">
                <button
                    className="btn btn-secondary dropdown-toggle"
                    type="button"
                    id="adminMenu"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                >
                    Админ
                </button>
                <ul className="dropdown-menu" aria-labelledby="adminMenu">
                    <li><Link className="dropdown-item" to="/admin/cars">Машины</Link></li>
                    <li><Link className="dropdown-item" to="/admin/users">Пользователи</Link></li>
                    <li><Link className="dropdown-item" to="/admin/orders">Заказы</Link></li>
                </ul>
            </div>)
    }
}

export default Header;