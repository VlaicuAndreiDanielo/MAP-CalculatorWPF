# 🧮 WPF Calculator

This is a calculator application developed using **C# and WPF (Windows Presentation Foundation)**.  
It replicates and extends the functionality of the Windows Calculator by offering **multiple operation modes**, **custom themes**, **keyboard + GUI input**, **memory/history tracking**, and **persistent settings**.

## 🧠 Modes

- **Standard Mode**
- **Scientific Mode**
- **Programmer Mode**
- **Equation Mode**

Each mode supports both **keyboard input** and **on-screen button input**.

## 🎨 Themes

The calculator comes with **10 color themes**:

1. **Heaven Light** – Soft, bright aesthetic
2. **Total Darkness** – Full dark mode for low-light use
3. **Deep Blue** – Calm, ocean-like blue palette
4. **Forest Green** – Earthy, soothing green tones
5. **Deep Orange** – Warm, vivid orange interface
6. **Sunny Yellow** – Energetic and bright yellow theme
7. **Pale Lavander** – Light purple, elegant look
8. **Intense Violet** – Deep purple, vibrant and bold
9. **Quantum Red** – Strong, futuristic red palette
10. **Baby Pink** – Soft and friendly light pink style

Both **History** and **Memory** interfaces apply the selected theme.

## 🧾 History and Memory

All calculator modes have:

- An **operation history**
- A **memory stack**, which supports:
  - `MS` – Store the current value in memory
  - `M+` – Add the current value to the existing memory value
  - `M-` – Subtract the current value from memory
  - `MR` – Recall the last stored memory value
  - `MC` – Clear all memory
  - `M🠷` – View the full memory stack, where you can select and reuse stored values

## 🧮 Mode Details

### 🔹 Standard Calculator
Performs:
- Basic arithmetic: `+`, `-`, `*`, `/`, `%`, `√`
- Memory operations: `MS`, `M+`, `M-`, `MR`, `MC`, `M🠷`
- Supports **digit grouping** (e.g., `1,000`)
- Supports **decimal point input** using `.` and regional formatting
- Supports **operation chaining** (e.g., `2 + 3 - 1 = 4`)

### 🔬 Scientific Calculator
Includes all features of the Standard Calculator, plus:
- Scientific functions:
- **log** – base-10 logarithm
- **ln** – natural logarithm (base *e*)
- **sin, cos, tan, cot** – trigonometric functions
- **x^2** – square of a number
- **x√y** – y-th root of x
- **n!** – factorial of a number
- **exp** – exponential function (e^x)
- **Parentheses `()` are used to concatenate multiple digits or values as a single text block**  

### 💻 Programmer Calculator
Specifically designed for programmers and developers, it includes:
- Arithmetic: `+`, `-`, `*`, `/`, `%`
- **Parentheses `()` are used to concatenate multiple digits or values as a single text block**  
- Bitwise operations:
  - `AND`, `OR`, `XOR`, `NOT`
  - `NAND`, `NOR`
- Bit shifting: `<<` (left shift), `>>` (right shift)
- Base conversions:
  - Decimal (DEC)
  - Binary (BIN)
  - Hexadecimal (HEX)
  - Octal (OCT)
- Allows switching between number bases at any time
- Digit grouping is applied even for non-decimal bases if enabled

### ➕ Equation Calculator
- Allows writing and evaluating full mathematical expressions
- Uses **postfix notation** to evaluate respecting **operator precedence**
- Result is shown only when `=` is pressed

## 🧰 Additional Features

### ✂️ Clipboard Support
- **Cut, Copy, and Paste** functions are implemented manually using string operations.
- These work with system clipboard, but not with default textbox context menus.
  
### 🧮 Digit Grouping
- Automatically adds separators between groups of digits (e.g., `1,000` or `1.000`) depending on system locale.
- Can be toggled on/off from the menu.
- When enabled, it also applies to numbers displayed in different bases (BIN, HEX, etc.)

## 🔒 Persistent Settings

The app remembers the following user settings even after closing:
1. Whether **Digit Grouping** is enabled
2. Last selected **calculator mode** (Programmer, Standard, Scientific, Ecuation)
3. Last selected **number base** in Programmer Mode (HEX, BIN, OCT, DEC)

Settings are saved in an encoded file or using .NET’s native user settings API.

## 🧰 Menu Options

- **File Menu:**
  - Cut, Copy, Paste
  - Toggle Digit Grouping
- **Help Menu:**
  - History
  - About (shows developer name and group)

## ⚠️ Input & Error Handling

- Supports full **keyboard and on-screen input**
- Expression chaining and immediate evaluation
- Handles invalid operations like:
  - Division by zero
  - Invalid input format (e.g., letters in numbers)
- ESC key acts like the "C" button – clears current operation

## 🚫 Restrictions & UI Behavior

- The calculator window cannot be resized
- Input fields are **not editable** text boxes
- Special keys like Enter (=) and ESC are handled explicitly
