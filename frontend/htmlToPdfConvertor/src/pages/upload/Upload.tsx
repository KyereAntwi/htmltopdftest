import { ColorModeButton } from '@/components/ui/color-mode';
import useUploadFile from '@/hooks/useUploadFile';
import {
  Flex,
  Box,
  HStack,
  Heading,
  Input,
  Button,
  Text,
} from '@chakra-ui/react';
import { useRef, useState } from 'react';

export default function Upload() {
  const [error, setError] = useState<string>('');
  const fileInputRef = useRef<HTMLInputElement>(null);

  const uploadMutation = useUploadFile(setError);

  const handleFileUpload = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const files = event.target.files;

    if (files) {
      const file = files[0];

      if (file.type !== 'text/html') {
        setError('Please upload a valid HTML file.');
        return;
      } else {
        setError('');
      }

      const newFormData = new FormData();
      newFormData.append('htmlFile', file);
      uploadMutation.mutateAsync(newFormData);

      if (fileInputRef.current) {
        fileInputRef.current.value = '';
      }
    }
  };

  return (
    <Flex
      as={'section'}
      flexDirection={'row'}
      width={'full'}
      h={'100vh'}
      justifyContent={'center'}
      alignItems={'center'}
    >
      <Box
        padding={{
          sm: '5px',
          md: '20px',
        }}
        borderRadius={'10px'}
      >
        <HStack
          width={'full'}
          justifyContent={'center'}
          alignItems={'center'}
          py={'5px'}
        >
          <Text>Toggle Color Theme</Text>
          <ColorModeButton />
        </HStack>
        <HStack
          width={'full'}
          justifyContent={'center'}
          alignItems={'center'}
          height={'150px'}
        >
          <Heading
            fontSize={{
              sm: 'lg',
              md: '2xl',
            }}
          >
            HTML to Pdf Convertor
          </Heading>
        </HStack>

        <HStack width={'full'} justifyContent={'center'} alignItems={'center'}>
          <Input
            ref={fileInputRef}
            type='file'
            as={Button}
            variant='outline'
            size='sm'
            accept='.html'
            onChange={handleFileUpload}
            disabled={uploadMutation.isPending}
          ></Input>
        </HStack>

        <HStack width={'full'} justifyContent={'center'} alignItems={'center'}>
          {error != '' && <Text color={'red'}>{error}</Text>}
          {uploadMutation.isPending && (
            <Text color={'green'} fontWeight={'bold'}>
              we are uploading your file for processing ...
            </Text>
          )}
        </HStack>
      </Box>
    </Flex>
  );
}
