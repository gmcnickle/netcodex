# Equality Semantics in .NET

> 🗃️ *A chapter from The .NET Codex*  
> *Because real understanding doesn’t come with a job title.*  

> *Not all equals are created equal.*

Equality in .NET is one of those topics that *looks* simple — until your code starts misbehaving.

`==` means equal, right? `.Equals()`... also means equal? And `ReferenceEquals()` — wait, when would you ever use that?

The reality is that .NET equality is full of traps: operator overloads, value vs. reference semantics, boxing, `IEquatable<T>`, and assumptions that don't hold up in practice. It's easy to get wrong — and even easier to never realize you did.

This article isn’t a formal deep-dive into the C# equality system. Instead, it’s a hands-on guide to:
- How equality works across value types, reference types, and immutable objects.
- When to override equality members — and when to avoid them.
- The practical risks of skipping `GetHashCode()`, relying on `==`, or trusting reference equality in the wrong context.
- Techniques (like tuple comparisons and `record` types) to get it right with less pain.

If you’ve ever been surprised by a failed `.Equals()` check, a weird `HashSet` bug, or a pull request that “looks right” but breaks equality logic — this is for you.

> 📌 *Looking for a true deep-dive into how `==`, `.Equals()`, and `ReferenceEquals()` behave under the hood? Stay tuned — that chapter’s coming soon.*
