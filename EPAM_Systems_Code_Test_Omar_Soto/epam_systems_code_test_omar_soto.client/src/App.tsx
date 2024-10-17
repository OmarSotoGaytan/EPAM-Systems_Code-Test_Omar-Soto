import { ToastContainer } from "react-toastify";
import { TextProcessorPage } from "./pages/TextProcessorPage";
import 'react-toastify/dist/ReactToastify.css';

function App() {

    return (
        <div>
            <ToastContainer
                position="top-right"
                autoClose={3000}
                hideProgressBar={false}
                newestOnTop={false}
                closeOnClick
                theme="light"
            />
            <TextProcessorPage />
        </div>
    );
}

export default App;