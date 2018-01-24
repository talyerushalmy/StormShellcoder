# Storm Shellcoder
Compose, test, and format your shellcodes easily and efficiently!

*Please notice*: this file describes the project in present time, and will be updated as more features are added.

**Features**

 - `Assemble` - Write assembly code and assemble it to opcodes
 - `Test` - Perform tests on the output opcodes
 - `Format` - Format the output opcodes to match your needs
 - `Copy` - Copy your masterpiece to the clipboard

**Assembler**

The current assembler used for this project is NASM (The Netwide Assembler).
In order for Storm Shellcoder to work, make sure `nasm.exe` is in your path.
In the future more assemblers will be supported.

**Tests**

The supported tests are the following:

- The assembler finished running successfully (`ASM`)
- The output shellcode contains a null byte (`\0`)

**Format**

Join sequence between bytes and control opcode configurations, e.g.
 > `0x13, 0x37` join on `\x`, add to beginning -> `\x13\x37`
 > `0x13, 0x37` join on `-`, don't add to ends -> `13-37`
 > `0x13, 0x37` join on `|`, add to beginning and end -> `|13|37|`

Add a prefix and a suffix to your output, e.g.
> `\x13\x37` with prefix `python -c "print '` and suffix `'" | ./program` -> `python -c "print '\x13\x37'" | ./program`

Use capital letters for your opcodes, e.g.
> `0xca, 0xfe, 0xba, 0xbe` capitalized -> `\xCA\xFE\xBA\xBE`
