// This is ExFat, an exFAT accessor written in pure C#
// Released under MIT license
// https://github.com/picrap/ExFat

namespace ExFat.Buffers;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

/// <summary>
/// Represents bytes in the buffer
/// </summary>
[DebuggerDisplay("{" + nameof(DebugLiteral) + "}")]
public readonly struct BufferBytes : IEnumerable<byte>
{
    private readonly Memory<byte> _buffer;

    /// <summary>
    /// Gets or sets the <see cref="byte"/> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="byte"/>.
    /// </value>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// </exception>
    public byte this[int index]
    {
        get => _buffer.Span[index];
        set => _buffer.Span[index] = value;
    }

    private string DebugLiteral
    {
        get
        {
            var s = string.Join(", ", this.Take(10).Select(b => $"0x{b:X2}"));
            if (_buffer.Length > 10)
            {
                s += " ...";
            }

            return s;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BufferBytes"/> class.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    public BufferBytes(Memory<byte> buffer)
    {
        _buffer = buffer;
    }

    /// <summary>
    /// Sets the specified bytes.
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    public void Set(ReadOnlySpan<byte> bytes)
    {
        for (var offset = 0; offset < _buffer.Length; offset++)
        {
            _buffer.Span[offset] = bytes[offset];
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<byte> GetEnumerator() => MemoryMarshal.ToEnumerable<byte>(_buffer).GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}