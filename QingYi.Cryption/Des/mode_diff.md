# Mode diff

CBC, CTS, or CFB is preferred.
优先选择CBC、CTS或CFB。

## CBC

The Cipher Block Chaining (CBC) mode introduces feedback. Before each plain text block is encrypted, it is combined with the cipher text of the previous block by a bitwise exclusive OR operation. This ensures that even if the plain text  contains many identical blocks, they will each encrypt to a different cipher text block. The initialization vector is combined with the first plain text block by a bitwise exclusive OR operation before the block is encrypted. If a single bit of the cipher text block is mangled, the corresponding plain text block will also be mangled. In addition, a bit in the subsequent block, in the same position as the original mangled bit, will be mangled.

密码方块链(CBC)模式引入了反馈。在对每个明文方块进行加密之前，将其与前一个方块的密文通过位异或操作进行组合。这确保了即使纯文本包含许多相同的块，它们也会被加密成不同的密文方块。在对方块进行加密之前，通过按位异或操作将初始化向量与第一个纯文本方块组合。如果密文方块的单个位被打乱，相应的纯文本方块也会被打乱。此外，在随后的方块中，与原始损坏位相同位置的位将被损坏。

## ECB

The Electronic Codebook (ECB) mode encrypts each block individually. Any blocks of plain text that are identical and in the same message, or that are in a different message encrypted with the same key, will be transformed into identical cipher text blocks. **Important:** This mode is not recommended because it opens the door for multiple security exploits.  If the plain text to be encrypted contains substantial repetition, it is feasible for the cipher text to be broken one block at a time.  It is also possible to use block analysis to determine the encryption key.  Also, an active adversary can substitute and exchange individual blocks without detection, which allows blocks to be saved and inserted into the stream at other points without detection.

电子密码本(ECB)模式对每个方块单独加密。在同一消息中相同的纯文本块，或者在使用相同密钥加密的不同消息中相同的纯文本块，将被转换为相同的密文块。**重要提示:** 不建议使用此模式，因为它可能导致多种安全漏洞。如果要加密的纯文本包含大量重复，则可以一次破解一个方块的密文。还可以使用方块分析来确定加密密钥。此外，主动攻击者可以在没有检测的情况下替换和交换单个块，这允许在没有检测的情况下将块保存并插入到流的其他点。

## OFB

The Output Feedback (OFB) mode processes small increments of plain text into cipher text instead of processing an entire block at a time. This mode is similar to CFB; the only difference between the two modes is the way that the shift register is filled. If a bit in the cipher text is mangled, the corresponding bit of plain text will be mangled. However, if there are extra or missing bits from the cipher text, the plain text will be mangled from that point on.

输出反馈(OFB)模式将少量的纯文本处理成密文，而不是一次处理整个方块。这种模式类似于CFB；两种模式之间的唯一区别是移位寄存器填充的方式。如果密文中的一个位被打乱，那么相应的明文位也会被打乱。但是，如果密文中有多余的位或缺少位，那么从那时起明文就会被打乱。

## CFB

The Cipher Feedback (CFB) mode processes small increments of plain text into cipher text, instead of processing an entire block at a time.  This mode uses a shift register that is one block in length and is divided into sections.  For example, if the block size is 8 bytes, with one byte processed at a time, the shift register is divided into eight sections.  If a bit in the cipher text is mangled, one plain text bit is mangled and the shift register is corrupted.  This results in the next several plain text increments being mangled until the bad bit is shifted out of the shift register.  The default feedback size can vary by algorithm, but is typically either 8 bits or the number of bits of the block size.  You can alter the number of feedback bits by using the `System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize` property.  Algorithms that support CFB use this property to set the feedback.

密码反馈(CFB)模式将少量的明文增量处理为密文，而不是一次处理整个方块。这种模式使用一个移位寄存器，它的长度是一个方块，并被分成几个部分。例如，如果方块大小为8字节，每次处理一个字节，则移位寄存器被划分为八个部分。如果密文中的一个位被打乱，则一个纯文本位被打乱，移位寄存器被损坏。这导致接下来的几个纯文本增量被打乱，直到坏的位被移出移位寄存器。默认的反馈大小可以因算法而异，但通常是8位或方块大小的位数。你可以使用`System.Security.Cryptography.SymmetricAlgorithm.FeedbackSize`属性来改变反馈比特的数量。支持CFB的算法使用此属性来设置反馈。

## CTS

The Cipher Text Stealing (CTS) mode handles any length of plain text and produces cipher text whose length matches the plain text length. This mode behaves like the CBC mode for all but the last two blocks of the plain text.

密文窃取(CTS)模式可以处理任意长度的明文，并生成长度与明文长度匹配的密文。除了纯文本的最后两个块之外，此模式的行为与CBC模式类似。
