import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Upload from './pages/upload/Upload';

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Upload />
    </QueryClientProvider>
  );
}

export default App;
