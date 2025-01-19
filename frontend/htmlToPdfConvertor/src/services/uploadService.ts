const BASE_URL: string = 'https://localhost:7164/api';

export const upload = async (data: FormData) => {
  try {
    const response = await fetch(`${BASE_URL}/HtmlToPdf/convert`, {
      method: 'POST',
      body: data,
    });

    if (!response.ok) {
      throw new Error('Network respone was not ok');
    }

    const blob = await response.blob();
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');

    a.href = url;
    a.download = 'converted-file.pdf';
    document.body.appendChild(a);
    a.click();
    a.remove();
    window.URL.revokeObjectURL(url);
  } catch (error) {
    console.error('There was a problem with the upload operation:', error);
    throw error;
  }
};
