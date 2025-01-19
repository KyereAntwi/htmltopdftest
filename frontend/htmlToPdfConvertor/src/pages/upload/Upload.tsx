import { ColorModeButton } from '@/components/ui/color-mode';
import { upload } from '@/services/uploadService';
import {
  Flex,
  Box,
  HStack,
  Heading,
  Input,
  Button,
  Text,
} from '@chakra-ui/react';

export default function Upload() {
  const handleFileUpload = (event: React.ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files;

    if (files) {
      const newFormData = new FormData();
      newFormData.append('htmlFile', files[0]);
      upload(newFormData);
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
            type='file'
            as={Button}
            variant='outline'
            size='sm'
            accept='.html'
            onChange={handleFileUpload}
          ></Input>
        </HStack>
      </Box>
    </Flex>
  );
}
