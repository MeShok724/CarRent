import { Link } from 'react-router-dom'

function Header() {
    return (
        <header style={{ background: '#333', color: '#fff', padding: '1rem' }}>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                <Link to="/"><h2 style={{ margin: 0, paddingLeft: 100, color: '#fff' }}>Car Rent</h2></Link>
                <nav>
                    <Link to="/" style={{ color: '#fff', marginRight: '1rem' }}>Главная</Link>
                    <Link to="/about" style={{ color: '#fff' }}>О нас</Link>
                </nav>
            </div>
        </header>
    )
}

export default Header;