import { upload } from '@/services/uploadService';
import { useMutation } from '@tanstack/react-query';

export default function useUploadFile(setError: (error: string) => void) {
  return useMutation({
    mutationFn: (data: FormData) => {
      return upload(data);
    },

    onError: (error: Error) => {
      setError(error.message);
    },
  });
}
